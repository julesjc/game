using SFML.Window;

namespace Game
{
    static class App
    {

        static void Main()
        {
            AppManager.Run("App", new VideoMode(1024, 768), new IntroScene());
        }

    }
}