using SFML.Graphics;
using SFML.System;

namespace Game
{
    public abstract class RectCollidedObject2D : Object2D
    {
        private Vector2f hitbox;
        private bool isFromGlobalBounds;
        public RectCollidedObject2D(Vector2f? hitbox = null) : base()
        {
            FloatRect globalBounds = GetSprite().GetGlobalBounds();
            if (hitbox != null)
            {
                this.hitbox = (Vector2f)hitbox;
            }
            else
            {
                this.hitbox = new Vector2f(globalBounds.Height, globalBounds.Width);
                isFromGlobalBounds = true;
            }

        }

        public Vector2f GetHitbox()
        {
            return hitbox;
        }


        public void SetHitbox(Vector2f hb)
        {
            hitbox = hb;
        }

        public override void SetTexture(Texture texture)
        {
            base.SetTexture(texture);
            if (isFromGlobalBounds)
            {
                FloatRect globalBounds = GetSprite().GetGlobalBounds();
                hitbox = new Vector2f(globalBounds.Height, globalBounds.Width);

            }
        }


        public FloatRect GetGlobalHitbox()
        {
            return new FloatRect(GetPos().X + hitbox.X / 2, GetPos().Y + hitbox.Y / 2, hitbox.X, hitbox.Y);
        }
    }
}
