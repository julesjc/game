
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    class Player : CircleCollidedObject2D
    {

        private int speed = 10;
        private float shotFrameCounter = 0;
        private static Player instance;
        private static Sound shotSound = AudioManager.LoadSound("data/sound/punch.ogg");
        private static Texture[] textures = new Texture[] { new Texture("data/sprites/player.png"), new Texture("data/sprites/player2.png"), new Texture("data/sprites/player3.png"), new Texture("data/sprites/player4.png") };

        public Player() : base(10)
        {
            instance = this;

            Animation[] animations = new Animation[] {
            new Animation("walking", new Texture[] { textures[0], textures[1] }, 10),
            new Animation("idle", new Texture[] { textures[2], textures[3] }, 60)
            };

            BindAnimations(animations);
            shotSound.Pitch = 0.5f;
        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
            {
                speed = 5;
            }
            else
            {
                speed = 10;
            }

            Vector2f playerDirection = new(0, 0);

            if (GetPos().X < App.screenSize.X & Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                playerDirection += VectorConstants.Left;
                SetScale(new Vector2f(1, 1));
            }
            else if (GetPos().X > 0 & Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                playerDirection -= VectorConstants.Left;
                SetScale(new Vector2f(-1, 1));
            }

            if (GetPos().Y < App.screenSize.Y & Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                playerDirection += VectorConstants.Up;
            }
            else if (GetPos().Y > 0 & Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                playerDirection -= VectorConstants.Up;
            }

            bool isMoving = playerDirection != VectorConstants.Zero;

            if (isMoving)
            {
                GetAnimationController()?.SetAnimation("walking");
            }
            else
            {
                GetAnimationController()?.SetAnimation("idle");
            }

            Transform(playerDirection * speed);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if (shotFrameCounter <= 0)
                {
                    new Shot(GetPos(), !isMoving ? new(GetScale().X, 0) : playerDirection, 30, false).Bind();
                    shotSound.Play();
                    shotFrameCounter = 5;
                }
                else
                {
                    shotFrameCounter--;
                }
            }
            else
            {
                shotFrameCounter = 0;
            }

        }

        public static Player getInstance()
        {
            return instance;
        }
    }
}