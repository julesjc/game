using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Enemy : CircleColliderObject2D
    {
        private float speed = 2;
        private bool dead;
        public static Sound deathSound = AudioManager.LoadSound("data/sound/vladmarche.ogg");
        FramesTimer actions;

        public Enemy(Vector2f pos) : base(100)
        {
            Texture[] textures = new Texture[2] { new Texture("data/sprites/vlad1.png"), new Texture("data/sprites/vlad2.png") };
            Animation[] animations = new Animation[] { new Animation("running", textures, 10), new Animation("stop", new Texture[1] { textures[0] }, 60) };
            BindAnimations(animations).SetAnimation("running");
            SetPos(pos);

            actions = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 50, () => {speed = 0; this.GetAnimationController()?.SetAnimation("stop"); new Shot(GetPos(), GraphicsUtils.GetDirection(GetPos(), Player.getInstance().GetPos())*7, true).Bind();} },
                { 100, () => {speed = RandomUtils.GetBetweenRange(5, 8); this.GetAnimationController()?.SetAnimation("running");} }
            }
            , false, true);

            actions.Bind();
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
                    App.app.Close();
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