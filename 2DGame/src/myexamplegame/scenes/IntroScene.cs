using System.Numerics;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    class IntroScene : BaseScene
    {
        //todo implement resource manager
        private static Music music = AudioManager.LoadMusic("data/sound/major_minor.ogg");
        private SceneText text = new SceneText();
        private string[] introText = new string[]{
            "placeholder",
        };
        public IntroScene() : base()
        {
            text.SetFont(new Font("data/Raleway-Medium.ttf"));
        }
        public override void Init()
        {

            new Background().Bind();
            text.SetPos(AppManager.GetScreenCenter());
            text.Bind();

            Vector2u windowSize = AppManager.GetScreenSize();

            int index = 0;
            text.SetString(introText[index]);
            FramesTimer step = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 200, () => {
                    index++;
                    if (index < introText.Length) {
                        text.SetString(introText[index]);
                    } else {
                        AppManager.ChangeScene(new MainScene());
                    }
                }},

            }, false, true);
            step.Bind();
            ;
            music.Loop = true;
            music.Play();
        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                AppManager.ChangeScene(new MainScene());
            }
        }

        public override void Unload()
        {
            base.Unload();
            music.Stop();
        }
    }
}