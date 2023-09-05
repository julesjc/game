namespace Game
{
    public abstract class CircleColliderSprite : CircleCollidedSprite, ICollider
    {
        private List<BaseSceneObject> collidingObjects;
        private List<BaseSceneObject> layer;
        public CircleColliderSprite(float? hitRadius = null, int layerReference = 0) : base(hitRadius)
        {
            collidingObjects = new List<BaseSceneObject>();
            layer = AppManager.GetCurrentScene<BaseScene>()?.GetLayer(layerReference) ?? new List<BaseSceneObject>();
        }
        override public void Update()
        {
            base.Update();

            foreach (BaseSceneObject obj in layer)
            {
                if (obj != this && CollisionUtils.IsCircleColliderCollidesObject(this, obj))
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
