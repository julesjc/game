namespace Game
{
    public abstract class BaseSceneObject : IBase
    {
        private bool dead;
        private int currentLayer;

        public virtual void Init()
        {
            //optional implementation
        }
        public virtual void Unload()
        {
            //optional implementation
        }
        public virtual void Update()
        {
            //optional implementation
        }

        public void SetCurrentLayer(int layer)
        {
            currentLayer = layer;
        }

        public int GetCurrentLayer()
        {
            return currentLayer;
        }

        public BaseSceneObject Bind(int layer = 0)
        {
            AppManager.GetCurrentScene<BaseScene>()?.Bind(this, layer);
            return this;
        }

        public bool IsDead()
        {
            return dead;
        }

        public void Die()
        {
            dead = true;
            Unload();
        }

        public void Reset()
        {
            dead = false;
            Init();
        }
    }
}
