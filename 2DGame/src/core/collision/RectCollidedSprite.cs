using SFML.Graphics;
using SFML.System;

namespace Game
{
    public abstract class RectCollidedSprite : SceneSprite
    {
        private Vector2f hitboxSize, hitboxOffsetFromOrigin;
        private bool isFromGlobalBounds;
        public RectCollidedSprite(Vector2f? hitboxSize = null, Vector2f? hitboxOffsetFromOrigin = null) : base()
        {
            FloatRect globalBounds = GetSprite().GetGlobalBounds();
            if (hitboxSize != null)
            {
                this.hitboxSize = (Vector2f)hitboxSize;
            }
            else
            {
                this.hitboxSize = new Vector2f(globalBounds.Height, globalBounds.Width);
                isFromGlobalBounds = true;
            }

            this.hitboxOffsetFromOrigin = hitboxOffsetFromOrigin ?? new Vector2f(0, 0);



        }

        public Vector2f GetHitboxSize()
        {
            return hitboxSize;
        }


        public void SetHitboxSize(Vector2f hb)
        {
            hitboxSize = hb;
            isFromGlobalBounds = false;
        }

        public Vector2f GetHitboxOffsetFromOrigin()
        {
            return hitboxOffsetFromOrigin;
        }


        public void SetHitboxOffsetFromOrigin(Vector2f offset)
        {
            hitboxOffsetFromOrigin = offset;
        }

        public override void SetTexture(Texture texture)
        {
            base.SetTexture(texture);
            if (isFromGlobalBounds)
            {
                FloatRect globalBounds = GetSprite().GetGlobalBounds();
                hitboxSize = new Vector2f(globalBounds.Width, globalBounds.Height);

            }
        }


        public FloatRect GetGlobalHitbox()
        {
            return new FloatRect(GetPos().X + hitboxOffsetFromOrigin.X - hitboxSize.X / 2, GetPos().Y + hitboxOffsetFromOrigin.Y - hitboxSize.Y / 2, hitboxSize.X, hitboxSize.Y);
        }
    }
}
