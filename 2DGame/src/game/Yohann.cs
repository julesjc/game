using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Yohann : RectColliderSprite
    {
        private float speed = 1, timeElapsed = 0;
        private bool dead;
        private static Sound deathSound = AudioManager.LoadSound("data/sound/vladmarche.ogg");
        private static Texture texture = new Texture("data/sprites/yo.png");

        public Yohann(Vector2f pos) : base()
        {
            SetTexture(texture);
            SetPos(pos);
        }

        public override void Update()
        {
            base.Update();

            if (dead)
            {
                SetScale(new(GetScale().X + GetScale().X / 10, GetScale().Y - 0.1f));
                if (GetScale().Y <= 0)
                {
                    Die();
                }
                return;
            }

            // Inside your update/render loop
            timeElapsed++;

            if (GetScale() == VectorConstants.DefaultScale)
            {
                timeElapsed = 0;
            }

            Vector2f wave = VectorUtils.Wave(timeElapsed, 0.1f, 0.1f);

            SetScale(wave);

            Vector2f playerPos = Player.getInstance().GetPos();

            MoveTowards(playerPos, speed);

        }

        public override void OnCollisionEnter(BaseSceneObject collided)
        {
            base.Collision(collided);

            if (!dead)
            {
                if (collided is Shot && !((Shot)collided).IsFromEnemy() && !dead)
                {
                    AudioManager.PlaySound(deathSound);
                    collided.Die();
                    dead = true;
                }
                else if (collided is Player)
                {
                    //App.app.Close();
                }
            }
        }

        public override void Unload()
        {
            base.Unload();
        }
    }
}