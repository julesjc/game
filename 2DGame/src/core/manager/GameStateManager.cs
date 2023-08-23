using System;
using System.Reflection;

namespace Game
{
	static class GameStateManager
	{
		private static BaseScene? currentScene = null;

		public static void Update()
		{
			currentScene?.Update();
		}

		public static void Draw()
		{
			currentScene?.Draw();
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

		public static BaseScene? GetCurrentScene()
		{
			return currentScene;
		}
	}
}
