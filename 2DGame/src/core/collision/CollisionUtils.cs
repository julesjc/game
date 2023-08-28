using SFML.System;
using SFML.Graphics;

namespace Game
{
	static class CollisionUtils
	{

		public static bool IsRectCollidable(IBase obj)
		{
			return obj != null && typeof(RectCollidedObject2D).IsAssignableFrom(obj.GetType());
		}


		public static bool IsCircleCollidable(IBase obj)
		{
			return obj != null && typeof(CircleCollidedObject2D).IsAssignableFrom(obj.GetType());
		}

		public static bool IsRectsContains(FloatRect rect, Vector2f point)
		{
			return rect.Contains(point.X, point.Y);
		}

		public static bool IsRectsCollision(FloatRect a, FloatRect b)
		{
			return a.Intersects(b);
		}

		public static bool IsCirclesCollision(Vector2f posA, Vector2f posB, float radiusA, float radiusB)
		{
			return VectorUtils.GetDistanceBetweenVectorsSquared(posA, posB) < MathUtils.GetSumSquared(radiusA, radiusB);
		}

		public static bool IsCircleRectCollision(Vector2f circlePos, float circleRadius, FloatRect rect)
		{
			Vector2f circleDistance = new Vector2f(Math.Abs(circlePos.X - rect.Left), Math.Abs(circlePos.Y - rect.Top));

			if ((circleDistance.X > (rect.Width / 2 + circleRadius)) || (circleDistance.Y > (rect.Height / 2 + circleRadius)))
			{
				return false;
			}

			if ((circleDistance.X <= (rect.Width / 2)) || (circleDistance.Y <= (rect.Height / 2)))
			{
				return true;
			}

			double cornerDistance_sq = Math.Sqrt(circleDistance.X - rect.Width / 2) + Math.Sqrt(circleDistance.Y - rect.Height / 2);

			return cornerDistance_sq <= Math.Sqrt(circleRadius);
		}

		public static bool IsRectColliderCollidesObject(RectColliderObject2D collider, BaseObject obj)
		{

			if (IsRectCollidable(obj))
			{
				return IsRectsCollision(collider.GetGlobalHitbox(), ((RectCollidedObject2D)obj).GetGlobalHitbox());
			}
			else if (IsCircleCollidable(obj))
			{
				CircleCollidedObject2D castObj = (CircleCollidedObject2D)obj;
				return IsCircleRectCollision(castObj.GetPos(), castObj.GetHitRadius(), collider.GetGlobalHitbox());
			}
			return false;
		}


		public static bool IsCircleColliderCollidesObject(CircleColliderObject2D collider, BaseObject obj)
		{
			if (IsCircleCollidable(obj))
			{
				CircleCollidedObject2D castObj = (CircleCollidedObject2D)obj;
				return IsCirclesCollision(collider.GetPos(), castObj.GetPos(), collider.GetHitRadius(), castObj.GetHitRadius());
			}
			else if (IsRectCollidable(obj))
			{
				RectCollidedObject2D castObj = (RectCollidedObject2D)obj;
				return IsCircleRectCollision(collider.GetPos(), collider.GetHitRadius(), castObj.GetGlobalHitbox());
			}
			return false;
		}
	}
}
