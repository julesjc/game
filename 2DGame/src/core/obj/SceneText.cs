using SFML.Graphics;
using SFML.System;

namespace Game
{
    public abstract class SceneText : BaseTransformable, IDrawable
    {
        private Text text;

        public SceneText()
        {
            text = new Text();
        }

        public void Draw()
        {
            App.app.Draw(text);
        }

        public override Vector2f GetPos()
        {
            return text.Position;
        }

        public override void SetPos(Vector2f pos)
        {
            text.Position = pos;
        }


        public override Vector2f GetScale()
        {
            return text.Scale;
        }

        public override void SetScale(Vector2f scale)
        {
            text.Scale = scale;
        }


        public override float GetRotation()
        {
            return text.Rotation;
        }

        public override void SetRotation(float degree)
        {
            text.Rotation = degree;
        }

        public Text GetText()
        {
            return text;
        }
    }
}
