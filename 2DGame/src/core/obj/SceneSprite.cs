using SFML.Graphics;
using SFML.System;

namespace Game
{
    public class SceneSprite : BaseTransformable, IDrawable
    {
        private bool hidden;
        private Sprite sprite;
        private AnimationController? animationController;

        public SceneSprite()
        {
            //todo generify to use font shape etc
            sprite = new Sprite();
        }

        public override void Update()
        {
            if (!hidden)
            {
                animationController?.updateTexture();
            }
        }

        public void Draw()
        {
            if (!hidden)
            {
                AppManager.GetWindow().Draw(sprite);
            }
        }

        public void Hide()
        {
            hidden = true;
        }

        public void Show()
        {
            hidden = false;
        }

        public override Vector2f GetPos()
        {
            return sprite.Position;
        }

        public override void SetPos(Vector2f pos)
        {
            sprite.Position = pos;
        }


        public override Vector2f GetScale()
        {
            return sprite.Scale;
        }

        public override void SetScale(Vector2f scale)
        {
            sprite.Scale = scale;
        }


        public override float GetRotation()
        {
            return sprite.Rotation;
        }

        public override void SetRotation(float degree)
        {
            sprite.Rotation = degree;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        public virtual void SetTexture(Texture texture)
        {
            sprite.Texture = texture;
            if (texture != null)
            {
                sprite.Texture.Smooth = true;
                sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
            }
        }

        public Texture GetTexture()
        {
            return sprite.Texture;
        }


        public AnimationController BindAnimations(Animation[] animations)
        {
            animationController = new AnimationController(this, animations);
            return animationController;
        }


        public AnimationController? GetAnimationController()
        {
            return animationController;
        }
    }
}
