using SFML.Graphics;

namespace Game
{
    public class Animation
    {
        private string id;
        private Texture[] textures;
        private int framesBetweenTextures;

        public Animation(string id, Texture[] textures, int framesBetweenTextures = 0)
        {
            this.textures = textures;
            this.framesBetweenTextures = framesBetweenTextures;
            this.id = id;
        }

        public Texture[] GetTextures()
        {
            return textures;
        }


        public int GetFramesBetweenTextures()
        {
            return framesBetweenTextures;
        }

        public string GetId()
        {
            return id;
        }
    }
}
