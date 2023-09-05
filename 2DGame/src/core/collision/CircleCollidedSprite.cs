using SFML.Graphics;

namespace Game
{
    public abstract class CircleCollidedSprite : SceneSprite
    {
        private float hitRadius;
        private bool isFromGlobalBounds, isEnabled;

        public CircleCollidedSprite(float? hitRadius = null) : base()
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
            return isEnabled ? hitRadius : 0;
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

        public void DisableCollision()
        {
            this.isEnabled = false;
        }

        public void EnableCollision()
        {
            this.isEnabled = true;
        }
    }
}
