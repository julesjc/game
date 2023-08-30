using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Ingress : RectColliderSprite
    {
        private bool dead;
        private static Texture[] textures = new Texture[2] { new Texture("data/sprites/shadow.png"), new Texture("data/sprites/shadow2.png") };
        FramesTimer actions;

        public Ingress(Vector2f pos) : base()
        {
            Animation[] animations = new Animation[] { new Animation("running", textures, 10), new Animation("stop", new Texture[1] { textures[0] }, 60) };
            BindAnimations(animations).SetAnimation("stop");
            SetPos(pos);

            actions = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 50, () => {
                        new Shot(GetPos(), VectorUtils.GetDirection(GetPos(), Player.getInstance().GetPos()), 5, true).Bind();
                    }
                },
            }
            , false, true);

            actions.Bind();
        }

        public override void Update()
        {

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
                if (collider is Shot && !((Shot)collider).IsFromEnemy() && !dead)
                {
                    collider.Die();
                    dead = true;
                }
                else if (collider is Player)
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