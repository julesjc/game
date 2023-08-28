using SFML.System;


namespace Game
{
    static class VectorUtils
    {
        public static Vector2f Lerp(Vector2f a, Vector2f b, float t)
        {
            return (1 - t) * a + t * b;
        }
        public static float Magnitude(Vector2f a)
        {
            return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }
        public static Vector2f Normalize(Vector2f a)
        {
            float mag = Magnitude(a);
            return new Vector2f(a.X / mag, a.Y / mag);
        }
        public static float GetDistanceBetweenVectorsSquared(Vector2f a, Vector2f b)
        {
            return (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
        }

        public static float GetRotationAngle(Vector2f pos, Vector2f direction)
        {
            double pAngle = Math.Atan2(pos.Y - direction.Y, pos.X - direction.X);
            return (float)(pAngle * 180 / Math.PI);
        }

        public static Vector2f GetDirection(Vector2f pos, Vector2f target)
        {
            Vector2f a = target - pos;
            float magnitude = Magnitude(a);
            if (magnitude == 0f)
            {
                return a;
            }
            return a / magnitude;
        }

        public static Vector2f AngleToVector(float degree)
        {
            float radians = degree * MathF.PI / 180;
            return new Vector2f((float)Math.Sin(radians), (float)Math.Cos(radians));
        }

        public static float VectorToAngle(Vector2f v)
        {
            if (v.X < 0)
            {
                return (float)(360 - (Math.Atan2(v.X, v.Y) * MathF.PI / 180 * -1));
            }
            else
            {
                return (float)Math.Atan2(v.X, v.Y) * MathF.PI / 180;
            }
        }

        public static Vector2f Wave(float timeElapsed, float frequency, float amplitude)
        {
            float sinScale = amplitude * (float)Math.Sin(frequency * timeElapsed);
            float cosScale = amplitude * (float)Math.Cos(frequency * timeElapsed);

            return new Vector2f(1.0f + sinScale, 1.0f + cosScale);
        }
    }
}
