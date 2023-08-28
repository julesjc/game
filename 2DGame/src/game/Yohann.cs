using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Yohann : CircleColliderObject2D
    {
        private float newSpeed, speed = 1, timeElapsed = 0;
        private bool dead;
        private static Sound deathSound = AudioManager.LoadSound("data/sound/vladmarche.ogg");
        private static Texture texture = new Texture("data/sprites/yohan.png");
        FramesTimer actions;

        public Yohann(Vector2f pos) : base(100)
        {
            SetTexture(texture);
            SetPos(pos);
            newSpeed = speed;

            List<Shot> lastShots = new List<Shot>();
            actions = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 100, () => {GetAnimationController()?.SetAnimation("running");} }
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

        public override void OnCollisionEnter(BaseObject collided)
        {
            base.Collision(collided);

            if (!dead)
            {
                if (collided.GetType() == typeof(Shot) && !((Shot)collided).IsFromEnemy() && !dead)
                {
                    AudioManager.PlaySound(deathSound);
                    collided.Die();
                    dead = true;
                }
                else if (collided.GetType() == typeof(Player))
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