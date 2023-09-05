using System.Numerics;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    class CongratulationsScene : BaseScene
    {
        private SceneText text = new SceneText();

        public CongratulationsScene() : base()
        {
            text.SetFont(new Font("data/Raleway-Medium.ttf"));
        }
        public override void Init()
        {

            new Background().Bind();
            text.SetPos(AppManager.GetScreenCenter());
            text.SetString("congrats placeholder");
            text.Bind();

        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                AppManager.GetWindow().Close();
            }
        }

        public override void Unload()
        {
            base.Unload();
        }
    }
}