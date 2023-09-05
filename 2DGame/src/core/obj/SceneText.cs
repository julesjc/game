using SFML.Graphics;
using SFML.System;

namespace Game
{
    public class SceneText : BaseTransformable, IDrawable
    {
        private Text text;

        public SceneText()
        {
            text = new Text();
        }

        public void Draw()
        {
            AppManager.GetWindow().Draw(text);
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

        public void SetString(string s)
        {
            text.DisplayedString = s;
            SetToCenter();
        }

        public void SetFont(Font font)
        {
            text.Font = font;
        }

        public Text GetText()
        {
            return text;
        }

        private void SetToCenter()
        {
            FloatRect textRect = text.GetLocalBounds();
            text.Origin = new(textRect.Left + textRect.Width / 2.0f, textRect.Top + textRect.Height / 2.0f);
        }
    }
}
