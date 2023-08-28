using SFML.Audio;
using SFML.System;

namespace Game
{
    class SampleScene : BaseScene
    {

        private int elapsed = 0;

        public static Music music = AudioManager.LoadMusic("data/sound/music.ogg");
        public override void Init()
        {

            new Background().Bind();

            Player obj = new Player();
            obj.SetPos(App.screenCenter);
            obj.Bind();

            Vector2f windowSize = App.app.DefaultView.Size;
            FramesTimer step = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 100 - elapsed, () => {new Yohann(new Vector2f(windowSize.X/2, windowSize.Y)).Bind(); }},
                { 150 - elapsed, () => {new Vlad(new Vector2f(windowSize.X/2, 0)).Bind(); }},
                { 200 - elapsed, () => {new Vlad(new Vector2f(0, windowSize.Y/2)).Bind(); }},
                { 350 - elapsed, () => {new Vlad(new Vector2f(windowSize.X, windowSize.Y/2)).Bind(); }},
                { 400 - elapsed, () => {elapsed --;} }

            }
            , false, true);
            step.Bind();

            music.Loop = true;
            // music.Play();
        }
    }
}