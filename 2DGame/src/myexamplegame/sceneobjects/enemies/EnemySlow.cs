using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class EnemySlow : RectColliderSprite
    {
        private float newSpeed, speed = 0;
        private bool dead;
        private static Sound deathSound = AudioManager.LoadSound("data/sound/walk.ogg");
        private static Texture[] textures = new Texture[2] { new Texture("data/sprites/vlad1.png"), new Texture("data/sprites/vlad2.png") };
        FramesTimer actions;

        public EnemySlow(Vector2f pos) : base()
        {
            Animation[] animations = new Animation[] { new Animation("running", textures, 10), new Animation("stop", new Texture[1] { textures[0] }, 60) };
            BindAnimations(animations).SetAnimation("stop");
            SetPos(pos);
            newSpeed = speed;

            List<Shot> lastShots = new List<Shot>();
            actions = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 50, () => {
                        newSpeed = 1;
                        GetAnimationController()?.SetAnimation("running");
                        for(int i =0;i <360; i++) {
                            if (i % 30 ==0) {
                                Shot shot = new Shot(GetPos(), VectorUtils.AngleToVector(i), 5, true);
                                lastShots.Add(shot);
                                AppManager.GetCurrentScene<MainScene>()?.AddToMap(shot);
                                shot.Bind();
                            }
                        }
                    }
                },
                { 60, () => {
                        foreach(Shot shot in lastShots) {
                            shot.SetDirection(VectorUtils.GetDirection(shot.GetPos(), Player.getInstance().GetPos()));
                            shot.SetSpeed(1);
                        }
                        lastShots.Clear();
                    }
                },
                { 100, () => {newSpeed = RandomUtils.GetBetweenRange(5, 8); GetAnimationController()?.SetAnimation("running");} }
            }
            , false, true);

            actions.Bind();
        }

        public override void Update()
        {

            speed = MathUtils.Lerpf(speed, newSpeed, 0.1f);

            if (dead)
            {
                SetScale(new(GetScale().X + GetScale().X / 10, GetScale().Y - 0.1f));
                if (GetScale().Y <= 0)
                {
                    Die();
                }
                return;
            }

            Vector2f playerPos = Player.getInstance().GetPos();

            MoveTowards(playerPos, speed);

            if (playerPos.X > GetPos().X)
            {
                SetScale(new Vector2f(1, 1));
            }
            else
            {
                SetScale(new Vector2f(-1, 1));
            }
            base.Update();
        }

        public override void OnCollisionEnter(BaseSceneObject collider)
        {
            base.Collision(collider);

            if (!dead)
            {
                if (collider is Shot shot && !shot.IsFromEnemy() && !dead)
                {
                    AudioManager.PlaySound(deathSound);
                    collider.Die();
                    dead = true;
                }
            }
        }

        public override void Collision(BaseSceneObject collided)
        {
            base.Collision(collided);

            if (collided is Tile || collided is EnemySlow)
            {
                CollisionUtils.ApplyRectRigidCollision(this, (RectCollidedSprite)collided);
            }

        }

        public override void Unload()
        {
            base.Unload();
            actions.Die();
        }

        public bool Dead()
        {
            return dead;
        }
    }
}