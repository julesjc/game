using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Shot : CircleCollidedObject2D
    {

        private Vector2f direction;
        private bool isFromEnemy;

        public Shot(Vector2f startPos, Vector2f direction, bool isFromEnemy) : base(2)
        {
            SetTexture(new Texture("data/Sprites/epee.png"));
            this.direction = direction;
            SetPos(startPos);
            SetScale(new Vector2f(0.2f, 0.2f));
            LookAt(GetPos() + direction);
            this.isFromEnemy = isFromEnemy;
        }

        public override void Update()
        {
            base.Update();
            Transform(direction);

            if (IsOutOfScreen())
            {
                Die();
            }
        }

        public bool IsFromEnemy()
        {
            return isFromEnemy;
        }
    }
}