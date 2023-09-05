using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class EnemyStatic : RectColliderSprite
    {
        private bool dead;
        private static Texture texture = new Texture("data/sprites/shadow.png");
        FramesTimer actions;
        List<Shot> shots = new List<Shot>();
        private static Sound deathSound = AudioManager.LoadSound("data/sound/dead.ogg");


        public EnemyStatic(Vector2f pos) : base()
        {
            SetTexture(texture);
            SetPos(pos);
        }

        public override void Init()
        {
            actions = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 100, () => {
                        shots.Add((Shot) new Shot(GetPos(), VectorUtils.GetDirection(GetPos(), Player.getInstance().GetPos()), 5, true, true).Bind());
                    }
                },
            }
            , false, true);

            actions.Bind();
        }

        public override void Update()
        {

            if (!dead)
            {
                GetSprite().Color = new Color((byte)RandomUtils.GetBetweenRange(0, 255), (byte)RandomUtils.GetBetweenRange(0, 255), (byte)RandomUtils.GetBetweenRange(0, 255));
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
                    SetRotation(90);
                    actions?.Die();
                    GetSprite().Color = Color.Blue;
                    dead = true;
                }
            }
        }

        public override void Unload()
        {
            base.Unload();
            foreach (Shot shot in shots)
            {
                shot.Die();
            }
            actions?.Die();
        }


        public bool Dead()
        {
            return dead;
        }
    }
}