using SFML.System;

namespace Game
{
    public abstract class RectColliderSprite : RectCollidedSprite, ICollider
    {
        private List<BaseSceneObject> collidingObjects;
        private List<BaseSceneObject> layer;
        public RectColliderSprite(Vector2f? hitbox = null, int layerReference = 0) : base(hitbox)
        {
            collidingObjects = new List<BaseSceneObject>();
            layer = AppManager.GetCurrentScene<BaseScene>()?.GetLayer(layerReference) ?? new List<BaseSceneObject>();
        }
        override public void Update()
        {
            base.Update();
            foreach (BaseSceneObject obj in layer.ToList())
            {
                if (obj != this && CollisionUtils.IsRectColliderCollidesObject(this, obj))
                {
                    if (!collidingObjects.Contains(obj))
                    {
                        OnCollisionEnter(obj);
                        collidingObjects.Add(obj);
                    }
                    Collision(obj);
                }
                else
                {
                    if (collidingObjects.Contains(obj))
                    {
                        collidingObjects.Remove(obj);
                        OnCollisionExit(obj);
                    }
                }

            }
        }
        public virtual void OnCollisionEnter(BaseSceneObject collided)
        {
            //optional implementation
        }
        public virtual void OnCollisionExit(BaseSceneObject collided)
        {
            //optional implementation
        }
        public virtual void Collision(BaseSceneObject collided)
        {
            //optional implementation
        }
    }
}
