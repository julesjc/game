namespace Game
{
    public class FramesTimer : BaseSceneObject
    {
        private Dictionary<int, Callback> events;
        private int framesElapsed;
        private bool dieAfterEvents, loop;
        public delegate void Callback();

        public FramesTimer(Dictionary<int, Callback> events, bool dieAfterEvents = false, bool loop = false)
        {
            this.dieAfterEvents = dieAfterEvents;
            this.events = events;
            this.loop = loop;
        }

        public override void Init()
        {
            base.Init();
            framesElapsed = 0;
        }

        public override void Update()
        {
            base.Update();
            if (events.ContainsKey(framesElapsed))
            {
                Callback callback = events[framesElapsed];
                callback();
                if (events.Last().Key == framesElapsed)
                {
                    if (dieAfterEvents)
                    {
                        Die();
                    }
                    else if (loop)
                    {
                        framesElapsed = 0;
                    }
                }
            }
            framesElapsed++;
        }
    }
}
