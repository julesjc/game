using SFML.Graphics;

namespace Game
{
    public class AnimationController
    {
        private Animation[] animations;
        private string? currentAnimationId;
        private int framesCount, animationIndex, framesToWait;
        private Object2D obj;
        private Texture[]? texturesToDisplay;
        public AnimationController(Object2D obj, Animation[] animations)
        {
            this.animations = animations;
            this.obj = obj;
        }
        public void updateTexture()
        {
            if (currentAnimationId != null && texturesToDisplay != null)
            {
                if (framesCount > 0)
                    framesCount--;
                else
                {
                    if (animationIndex >= texturesToDisplay.Length)
                    {
                        animationIndex = 0;
                    }
                    Texture currTexture = texturesToDisplay[animationIndex];
                    if (!currTexture.Equals(obj.GetTexture()))
                    {
                        obj.SetTexture(currTexture);
                    }
                    animationIndex++;
                    framesCount = framesToWait;
                }
            }
        }

        public void Reset()
        {
            framesCount = 0;
            animationIndex = 0;
        }

        public object? GetAnimation()
        {
            return currentAnimationId;
        }


        public void SetAnimation(string? id)
        {
            if (id != null && !id.Equals(currentAnimationId))
            {
                currentAnimationId = id;
                Animation? currAnimation = Array.Find(animations, animation => id.Equals(animation.GetId(), StringComparison.Ordinal));
                if (currAnimation != null)
                {
                    texturesToDisplay = currAnimation.GetTextures();
                    framesToWait = currAnimation.GetFramesBetweenTextures();
                    framesCount = 0;
                }
            }
        }

    }
}
