using SFML.Graphics;
using SFML.Window;
using SFML.System;
namespace Game
{
	static class AppManager
	{
		private static RenderWindow window;
		private static BaseScene? currentScene = null;

		public static RenderWindow GetWindow()
		{
			return window;
		}

		public static Vector2u GetScreenSize()
		{
			return window.Size;
		}

		public static Vector2f GetScreenCenter()
		{
			return new Vector2f(window.Size.X / 2, window.Size.Y / 2);
		}

		//entry point
		public static void Run(string appName, VideoMode resolution, BaseScene startScene)
		{
			window =  new RenderWindow(resolution, appName, Styles.Close);

			window.SetFramerateLimit(60);
			window.RequestFocus();

			window.Closed += (sender, e) => ((RenderWindow)sender).Close();

			ChangeScene(startScene);

			while (window.IsOpen)
			{
				currentScene?.Update();
				window.DispatchEvents();   // Clear screen
				window.Clear(new Color(0, 0, 0));  // Update the window
				currentScene?.Draw();
				window.Display();
			}
		}

		public static void ChangeScene(BaseScene newScene)
		{
			currentScene?.Unload();
			currentScene = null;
			if (newScene != null)
			{
				currentScene = newScene;
				newScene.Init();
			}
		}

		public static T? GetCurrentScene<T>() where T : BaseScene
		{
			return currentScene != null ? (T)currentScene : null;
		}
	}
}
