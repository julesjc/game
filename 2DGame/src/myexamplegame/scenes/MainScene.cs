using System.Numerics;
using SFML.Audio;
using SFML.System;

namespace Game
{
    class MainScene : BaseScene
    {

        private int indexBeforeWin = 0;
        private List<SceneSprite> map;
        private Player player;
        private bool canExit = false;
        private static Music music = AudioManager.LoadMusic("data/sound/loop.ogg");
        public override void Init()
        {

            new Background().Bind();

            player = new Player();
            player.SetPos(new(512, 256));
            player.Bind(1);

            music.Loop = true;
            music.Play();
            NewMap(player.GetPos(), false);
        }

        public override void Update()
        {
            base.Update();

            Vector2u windowSize = AppManager.GetScreenSize();
            Vector2f playerPos = player.GetPos();
            Vector2f playerHbSize = player.GetHitboxSize();

            if (canExit)
            {
                if (playerPos.X > windowSize.X - 1)
                {
                    player.SetPos(new(playerHbSize.X / 2, playerPos.Y));
                    NewMap(player.GetPos(), true);
                }
                else if (playerPos.X < 1)
                {
                    player.SetPos(new(windowSize.X - playerHbSize.X / 2, playerPos.Y));
                    NewMap(player.GetPos(), true);
                }
                else if (playerPos.Y > windowSize.Y - 1)
                {
                    player.SetPos(new(playerPos.X, playerHbSize.Y / 2));
                    NewMap(player.GetPos(), true);
                }
                else if (playerPos.Y < 1)
                {
                    player.SetPos(new(playerPos.X, windowSize.Y - playerHbSize.Y / 2));
                    NewMap(player.GetPos(), true);
                }
            }
        }

        public void NewMap(Vector2f playerPos, bool spawnMobs)
        {
            if (indexBeforeWin == 15)
            {
                AppManager.ChangeScene(new CongratulationsScene());
            }
            else
            {
                if (map != null)
                {
                    foreach (SceneSprite obj in map)
                    {
                        obj.Die();
                    }
                    map.Clear();
                }
                map = MapUtils.GetGeneratedMap(playerPos, spawnMobs);
                foreach (SceneSprite obj in map)
                {
                    obj.Bind();
                }
                canExit = false;
                indexBeforeWin++;
            }

        }

        public void SetPlayerCanExit(bool canExit)
        {
            this.canExit = canExit;
        }

        public void AddToMap(SceneSprite obj)
        {
            map.Add(obj);
        }

        public override void Unload()
        {
            base.Unload();
            music.Stop();
        }
    }
}