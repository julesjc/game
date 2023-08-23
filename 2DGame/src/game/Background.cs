
using SFML.Graphics;

namespace Game
{
    class Background : Object2D
    {
        public Background() : base()
        {
            SetTexture(new Texture("data/sprites/nevrose.png"));
            SetPos(App.screenCenter);
        }
    }
}