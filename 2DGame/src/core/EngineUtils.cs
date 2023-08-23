namespace Game
{
	static class EngineUtils
	{
		public static bool IsDrawable(IBase obj)
		{
			return obj != null && typeof(IDrawable).IsAssignableFrom(obj.GetType());
		}
	}
}
