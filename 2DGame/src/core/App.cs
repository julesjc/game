using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    static class App
    {
        public static RenderWindow app = new RenderWindow(new VideoMode(1024, 768), "Game", Styles.Close);
        public static bool canInput = false;
        public static Vector2f screenCenter = new Vector2f(app.Size.X / 2, app.Size.Y / 2);
        public static Vector2u screenSize = app.Size;
        static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
            return;
        }

        static void OnLostFocus(object sender, EventArgs e)
        {
            canInput = false;
            return;
        }
        static void OnGainedFocus(object sender, EventArgs e)
        {
            canInput = true;
            return;
        }

        static void Main()
        {
            //Clock clock = new Clock();

            app.SetFramerateLimit(60);
            app.RequestFocus();
            app.Closed += new EventHandler(OnClose);
            app.GainedFocus += new EventHandler(OnGainedFocus);
            app.LostFocus += new EventHandler(OnLostFocus);

            if (app.HasFocus())
            {
                canInput = true;
            }

            GameStateManager.ChangeScene(new SampleScene());

            while (app.IsOpen)
            {
                GameStateManager.Update();
                app.DispatchEvents();   // Clear screen
                app.Clear(new Color(0, 0, 0));  // Update the window
                GameStateManager.Draw();
                app.Display();

            }
        }
        //End Main()
    } //End Program
}