
using SFML.Graphics;

namespace Game
{
    class Background : Object2D
    {
        private static Texture texture = new Texture("data/sprites/nevrose.png");
        public Background() : base()
        {
            SetTexture(texture);
            SetPos(App.screenCenter);
        }
    }
}