using SFML.System;


namespace Game
{
    static class MathUtils
    {

        public static float Lerpf(float a, float b, float t)
        {
            return (a + t * (b - a));
        }
        public static int Lerpi(int a, int b, int t)
        {
            return (a + t * (b - a));
        }
        public static float GetSumSquared(float a, float b)
        {
            return (a + b) * (a + b);
        }
    }
}
