namespace Game
{
    public abstract class CircleColliderObject2D : CircleCollidedObject2D, ICollider
    {
        private List<BaseObject> collidingObjects;
        private List<BaseObject> layer;
        public CircleColliderObject2D(float? hitRadius = null, int layerReference = 0) : base(hitRadius)
        {
            collidingObjects = new List<BaseObject>();
            layer = GameStateManager.GetCurrentScene()?.GetLayer(layerReference) ?? new List<BaseObject>();
        }
        override public void Update()
        {
            base.Update();

            foreach (BaseObject obj in layer)
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
        public virtual void OnCollisionEnter(BaseObject obj)
        {
            //optional implementation
        }
        public virtual void OnCollisionExit(BaseObject obj)
        {
            //optional implementation
        }
        public virtual void Collision(BaseObject obj)
        {
            //optional implementation
        }
    }
}
