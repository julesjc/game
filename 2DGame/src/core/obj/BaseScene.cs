namespace Game
{
    abstract class BaseScene : IBase, IDrawable
    {
        private Dictionary<int, List<BaseSceneObject>> layers = new Dictionary<int, List<BaseSceneObject>>();
        public virtual void Init()
        {
            //optional implementation
        }
        public void Draw()
        {
            foreach (List<BaseSceneObject> layer in layers.Values.ToList())
            {
                foreach (BaseSceneObject obj in layer.ToList())
                {
                    if (EngineUtils.IsDrawable(obj))
                    {
                        ((IDrawable)obj).Draw();
                    }
                }
            }
        }
        public virtual void Update()
        {
            foreach (List<BaseSceneObject> layer in layers.Values.ToList())
            {
                layer.RemoveAll(obj => obj.IsDead());

                foreach (BaseSceneObject obj in layer.ToList())
                {
                    obj.Update();
                }
            }
        }
        public virtual void Unload()
        {
            layers.Clear();
        }

        public void Bind(BaseSceneObject obj, int layerId = 0)
        {
            if (!layers.ContainsKey(layerId))
            {
                layers.Add(layerId, new List<BaseSceneObject>());
            }
            List<BaseSceneObject> layer = layers[layerId];
            if (!layer.Contains(obj))
            {
                obj.SetCurrentLayer(layerId);
                obj.Reset();
                layers[layerId].Add(obj);
            }
        }

        public List<BaseSceneObject>? GetLayer(int index)
        {
            if (layers.ContainsKey(index))
            {
                return layers[index];
            }
            return null;
        }
    }
}
