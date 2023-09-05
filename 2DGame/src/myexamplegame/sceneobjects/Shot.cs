using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Shot : CircleColliderSprite
    {

        private Vector2f direction;
        private float newSpeed, speed;
        private bool isFromEnemy, bounce;
        private static Texture texture = new Texture("data/sprites/epee.png");

        public Shot(Vector2f startPos, Vector2f direction, float speed, bool isFromEnemy = true, bool bounce = false) : base(1)
        {
            this.direction = direction;
            this.isFromEnemy = isFromEnemy;
            this.bounce = bounce;
            this.speed = speed;
            newSpeed = speed;
            SetPos(startPos);
            LookAt(GetPos() + direction);
            SetTexture(texture);

        }

        public override void Update()
        {
            if (isFromEnemy)
            {
                speed = MathUtils.Lerpf(speed, newSpeed, 0.1f);
            }
            base.Update();
            Transform(direction * speed);

            if (IsOutOfScreen())
            {
                //Die();
            }
        }

        public void SetDirection(Vector2f direction)
        {
            this.direction = direction;
            LookAt(GetPos() + direction);

        }

        public void SetSpeed(float speed)
        {
            this.newSpeed = speed;
        }

        public bool IsFromEnemy()
        {
            return isFromEnemy;
        }

        public override void OnCollisionEnter(BaseSceneObject collided)
        {
            base.OnCollisionEnter(collided);
            if (collided is Tile tile && !tile.IsExit())
            {

                if (bounce)
                {
                    direction = VectorUtils.Reflect(GetPos(), ((Tile)collided).GetPos());
                    LookAt(GetPos() + direction);
                }
                else
                {
                    Die();
                }

            }
        }
    }
}