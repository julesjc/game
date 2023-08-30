using SFML.Graphics;

namespace Game
{
	static class EngineUtils
	{
		public static bool IsDrawable(IBase obj)
		{
			return obj is IDrawable;
		}
	}
}
