using System.Numerics;
using SFML.Audio;
using SFML.System;

namespace Game
{
    class SampleScene : BaseScene
    {

        private int elapsed = 0;
        private List<SceneSprite> map;
        private Player player;
        private bool canExit = false;
        public static Music music = AudioManager.LoadMusic("data/sound/music.ogg");
        public override void Init()
        {

            new Background().Bind();

            player = new Player();
            player.SetPos(new(512, 256));
            player.Bind(1);

            /*Vector2u windowSize = App.screenSize;
            FramesTimer step = new FramesTimer(new Dictionary<int, FramesTimer.Callback>()
            {
                { 100 - elapsed, () => {new Yohann(new Vector2f(windowSize.X/2, windowSize.Y)).Bind(); }},
                { 150 - elapsed, () => {new Vlad(new Vector2f(windowSize.X/2, 0)).Bind(); }},
                { 200 - elapsed, () => {new Vlad(new Vector2f(0, windowSize.Y/2)).Bind(); }},
                { 350 - elapsed, () => {new Vlad(new Vector2f(windowSize.X, windowSize.Y/2)).Bind(); }},
                { 400 - elapsed, () => {elapsed --;} }

            }
            , false, true);
            step.Bind();*/
            ;
            music.Loop = true;
            // music.Play();
            NewMap(player.GetPos());
        }

        public override void Update()
        {
            base.Update();

            Vector2u windowSize = App.screenSize;
            Vector2f playerPos = player.GetPos();
            Vector2f playerHbSize = player.GetHitboxSize();

            if (canExit)
            {
                if (playerPos.X > windowSize.X - 1)
                {
                    player.SetPos(new(playerHbSize.X / 2, playerPos.Y));
                    NewMap(player.GetPos());
                }
                else if (playerPos.X < 1)
                {
                    player.SetPos(new(windowSize.X - playerHbSize.X / 2, playerPos.Y));
                    NewMap(player.GetPos());
                }
                else if (playerPos.Y > windowSize.Y - 1)
                {
                    player.SetPos(new(playerPos.X, playerHbSize.Y / 2));
                    NewMap(player.GetPos());
                }
                else if (playerPos.Y < 1)
                {
                    player.SetPos(new(playerPos.X, windowSize.Y - playerHbSize.Y / 2));
                    NewMap(player.GetPos());
                }
            }
        }

        public void NewMap(Vector2f playerPos)
        {
            if (map != null)
            {
                foreach (SceneSprite obj in map)
                {
                    obj.Die();
                }
                map.Clear();
            }
            map = MapUtils.GetGeneratedMap(playerPos);
            foreach (SceneSprite obj in map)
            {
                obj.Bind();
            }
            canExit = false;
        }

        public void SetPlayerCanExit(bool canExit)
        {
            this.canExit = canExit;
        }
    }
}