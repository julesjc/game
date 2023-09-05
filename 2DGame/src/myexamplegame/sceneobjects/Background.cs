
using SFML.Graphics;

namespace Game
{
    class Background : SceneSprite
    {
        private static Texture texture = new Texture("data/sprites/background.png");
        public Background() : base()
        {
            SetTexture(texture);
            SetPos(AppManager.GetScreenCenter());
        }
    }
}