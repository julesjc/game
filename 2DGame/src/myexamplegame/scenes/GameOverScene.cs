using System.Numerics;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    class GameOverScene : BaseScene
    {
        private SceneText text = new SceneText();

        public GameOverScene() : base()
        {
            text.SetFont(new Font("data/Raleway-Medium.ttf"));
        }
        public override void Init()
        {

            new Background().Bind();
            text.SetPos(AppManager.GetScreenCenter());
            text.SetString("Game over");
            text.Bind();

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
        }
    }
}