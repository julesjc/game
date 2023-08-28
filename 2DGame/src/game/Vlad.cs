using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Vlad : CircleColliderObject2D
    {
        private float newSpeed, speed = 2;
        private bool dead;
        private static Sound deathSound = AudioManager.LoadSound("data/sound/vladmarche.ogg");
        private static Texture[] textures = new Texture[2] { new Texture("data/sprites/vlad1.png"), new Texture("data/sprites/vlad2.png") };
        FramesTimer actions;

        public Vlad(Vector2f pos) : base(100)
        {
            Animation[] animations = new Animation[] { new Animation("running", textures, 10), new Animation("stop", new Texture[1] { textures[0] }, 60) };
            BindAnimations(animations).SetAnimation("running");
            SetPos(pos);
            newSpeed = speed;

            List<Shot> lastShots = new List<Shot>();
            actions = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 50, () => {
                        newSpeed = 0;
                        GetAnimationController()?.SetAnimation("stop");
                        for(int i =0;i <360; i++) {
                            if (i % 10 ==0) {
                                lastShots.Add((Shot) new Shot(GetPos(), VectorUtils.AngleToVector(i), 5, true).Bind());
                            }
                        }
                    }
                },
                { 75, () => {
                        foreach(Shot shot in lastShots) {
                            shot.SetDirection(VectorUtils.GetDirection(shot.GetPos(), Player.getInstance().GetPos()));
                            shot.SetSpeed(4);
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
            base.Update();

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
        }

        public override void OnCollisionEnter(BaseObject collider)
        {
            base.Collision(collider);

            if (!dead)
            {
                if (collider.GetType() == typeof(Shot) && !((Shot)collider).IsFromEnemy() && !dead)
                {
                    AudioManager.PlaySound(deathSound);
                    collider.Die();
                    dead = true;
                }
                else if (collider.GetType() == typeof(Player))
                {
                    //App.app.Close();
                }
            }
        }

        public override void Unload()
        {
            base.Unload();
            actions.Die();
        }
    }
}