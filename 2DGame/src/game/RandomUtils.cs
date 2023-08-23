using SFML.System;

namespace Game
{
    static class RandomUtils
    {
        public static Random rand = new Random();
        public static Vector2f GetRandomVector(int a, int b)
        {
            return new Vector2f(GetBetweenRange(a, b), GetBetweenRange(a, b));
        }

        public static int GetBetweenRange(int a, int b)
        {
            return rand.Next(a, b);
        }
    }
}
