using SFML.System;
using SFML.Graphics;

namespace Game
{
	static class CollisionUtils
	{

		public static bool IsRectCollidable(IBase obj)
		{
			return obj is RectCollidedSprite;
		}


		public static bool IsCircleCollidable(IBase obj)
		{
			return obj is CircleCollidedSprite;
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
			Vector2f circleDistance = new Vector2f(
				Math.Abs(circlePos.X - (rect.Left + rect.Width / 2)),
				Math.Abs(circlePos.Y - (rect.Top + rect.Height / 2))
			);

			if (circleDistance.X > (rect.Width / 2 + circleRadius) || circleDistance.Y > (rect.Height / 2 + circleRadius))
			{
				return false;
			}

			if (circleDistance.X <= (rect.Width / 2) || circleDistance.Y <= (rect.Height / 2))
			{
				return true;
			}

			double cornerDistanceSq = Math.Pow(circleDistance.X - rect.Width / 2, 2) + Math.Pow(circleDistance.Y - rect.Height / 2, 2);

			return cornerDistanceSq <= Math.Pow(circleRadius, 2);
		}

		public static bool IsRectColliderCollidesObject(RectColliderSprite collider, BaseSceneObject obj)
		{

			if (IsRectCollidable(obj))
			{
				return IsRectsCollision(collider.GetGlobalHitbox(), ((RectCollidedSprite)obj).GetGlobalHitbox());
			}
			else if (IsCircleCollidable(obj))
			{
				CircleCollidedSprite castObj = (CircleCollidedSprite)obj;
				return IsCircleRectCollision(castObj.GetPos(), castObj.GetHitRadius(), collider.GetGlobalHitbox());
			}
			return false;
		}


		public static bool IsCircleColliderCollidesObject(CircleColliderSprite collider, BaseSceneObject obj)
		{
			if (IsCircleCollidable(obj))
			{
				CircleCollidedSprite castObj = (CircleCollidedSprite)obj;
				return IsCirclesCollision(collider.GetPos(), castObj.GetPos(), collider.GetHitRadius(), castObj.GetHitRadius());
			}
			else if (IsRectCollidable(obj))
			{
				RectCollidedSprite castObj = (RectCollidedSprite)obj;
				return IsCircleRectCollision(collider.GetPos(), collider.GetHitRadius(), castObj.GetGlobalHitbox());
			}
			return false;
		}

		public static void ApplyRectRigidCollision(RectColliderSprite collider, RectCollidedSprite rigidbody)
		{
			FloatRect colliderHitbox = collider.GetGlobalHitbox();
			FloatRect rigidBodyHitbox = rigidbody.GetGlobalHitbox();

			Vector2f colliderPos = collider.GetPos();
			Vector2f rigidbodyPos = rigidbody.GetPos();

			float overlapX = Math.Min(colliderHitbox.Left + colliderHitbox.Width, rigidBodyHitbox.Left + rigidBodyHitbox.Width) -
							 Math.Max(colliderHitbox.Left, rigidBodyHitbox.Left);
			float overlapY = Math.Min(colliderHitbox.Top + colliderHitbox.Height, rigidBodyHitbox.Top + rigidBodyHitbox.Height) -
							 Math.Max(colliderHitbox.Top, rigidBodyHitbox.Top);

			if (overlapX < overlapY)
			{
				if (colliderPos.X > rigidbodyPos.X)
				{
					colliderPos.X += overlapX;
				}
				else
				{
					colliderPos.X -= overlapX;
				}
			}
			else
			{
				if (colliderPos.Y > rigidbodyPos.Y)
				{
					colliderPos.Y += overlapY;
				}
				else
				{
					colliderPos.Y -= overlapY;
				}
			}

			collider.SetPos(colliderPos);
		}
	}
}
