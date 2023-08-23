namespace Game
{
    abstract class BaseScene : IBase, IDrawable
    {
        private Dictionary<int, List<BaseObject>> layers = new Dictionary<int, List<BaseObject>>();
        public virtual void Init()
        {
            //optional implementation
        }
        public void Draw()
        {
            foreach (List<BaseObject> layer in layers.Values.ToList())
            {
                foreach (BaseObject obj in layer.ToList())
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
            foreach (List<BaseObject> layer in layers.Values.ToList())
            {
                layer.RemoveAll(obj => obj.IsDead());

                foreach (BaseObject obj in layer.ToList())
                {
                    obj.Update();
                }
            }
        }
        public virtual void Unload()
        {
            foreach (List<BaseObject> layer in layers.Values.ToList())
            {
                foreach (BaseObject obj in layer.ToList())
                {
                    obj.Unload();
                }
            }
            layers.Clear();
        }

        public void Bind(BaseObject obj, int layerId = 0)
        {
            if (!layers.ContainsKey(layerId))
            {
                layers.Add(layerId, new List<BaseObject>());
            }
            List<BaseObject> layer = layers[layerId];
            if (!layer.Contains(obj))
            {
                obj.SetCurrentLayer(layerId);
                obj.Reset();
                layers[layerId].Add(obj);
            }
        }

        public List<BaseObject>? GetLayer(int index)
        {
            if (layers.ContainsKey(index))
            {
                return layers[index];
            }
            return null;
        }
    }
}
