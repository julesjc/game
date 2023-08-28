using SFML.Graphics;
using SFML.System;

namespace Game
{
    public abstract class Object2D : BaseObject, IDrawable
    {
        private bool hidden;
        private Sprite sprite;
        private AnimationController? animationController;

        public Object2D()
        {
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
                App.app.Draw(sprite);
            }
        }

        public void Transform(Vector2f v)
        {
            SetPos(GetPos() + v);
        }

        public void MoveTowards(Vector2f target, float speed)
        {
            Transform(VectorUtils.GetDirection(GetPos(), target) * speed);
        }

        public void LookAt(Vector2f direction)
        {
            SetRotation(VectorUtils.GetRotationAngle(GetPos(), direction));
        }

        public void Hide()
        {
            hidden = true;
        }

        public void Show()
        {
            hidden = false;
        }

        public Vector2f GetPos()
        {
            return sprite.Position;
        }

        public void SetPos(Vector2f pos)
        {
            sprite.Position = pos;
        }


        public Vector2f GetScale()
        {
            return sprite.Scale;
        }

        public void SetScale(Vector2f scale)
        {
            sprite.Scale = scale;
        }


        public float GetRotation()
        {
            return sprite.Rotation;
        }

        public void SetRotation(float degree)
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

        public bool IsOutOfScreen()
        {
            Vector2f screenSize = App.app.DefaultView.Size;
            Vector2f pos = GetPos();
            return !(pos.X > 0 && pos.X < screenSize.X && pos.Y > 0 && pos.Y < screenSize.Y);
        }
    }
}
