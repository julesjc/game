using SFML.Graphics;
using SFML.System;

namespace Game
{
    public abstract class BaseTransformable : BaseSceneObject
    {
        public abstract Vector2f GetPos();
        public abstract void SetPos(Vector2f pos);
        public abstract Vector2f GetScale();
        public abstract void SetScale(Vector2f scale);
        public abstract float GetRotation();
        public abstract void SetRotation(float degree);
        public bool IsOutOfScreen()
        {
            Vector2u screenSize = App.screenSize;
            Vector2f pos = GetPos();
            return !(pos.X > 0 && pos.X < screenSize.X && pos.Y > 0 && pos.Y < screenSize.Y);
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
    }
}
