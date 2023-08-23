using SFML.Graphics;

namespace Game
{
    public abstract class CircleCollidedObject2D : Object2D
    {
        private float hitRadius;
        private bool isFromGlobalBounds;

        public CircleCollidedObject2D(float? hitRadius = null) : base()
        {
            if (hitRadius != null)
            {
                this.hitRadius = (float)hitRadius;
            }
            else
            {
                this.hitRadius = GetSprite().GetGlobalBounds().Height / 2;
                isFromGlobalBounds = true;
            }

        }

        public float GetHitRadius()
        {
            return hitRadius;
        }


        public void SetHitRadius(float hr)
        {
            hitRadius = hr;
        }


        public override void SetTexture(Texture texture)
        {
            base.SetTexture(texture);
            if (isFromGlobalBounds)
            {
                hitRadius = GetSprite().GetGlobalBounds().Height / 2;
            }
        }
    }
}
