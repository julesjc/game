
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    class Player : RectColliderSprite
    {

        private int speed = 10;
        private float shotFrameCounter = 0;
        private Vector2f? lastMove;
        private static Player? instance = null;
        private static Sound shotSound = AudioManager.LoadSound("data/sound/punch.ogg");
        private static Texture[] textures = new Texture[] { new Texture("data/sprites/player.png"), new Texture("data/sprites/player2.png"), new Texture("data/sprites/player3.png"), new Texture("data/sprites/player4.png") };

        public Player() : base()
        {
            instance = this;

            Animation[] animations = new Animation[] {
            new Animation("walking", new Texture[] { textures[0], textures[1] }, 10),
            new Animation("idle", new Texture[] { textures[2], textures[3] }, 60)
            };

            BindAnimations(animations);
            GetAnimationController()?.SetAnimation("idle");
            shotSound.Pitch = 0.5f;
        }

        public override void Update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
            {
                speed = 2;
            }
            else
            {
                speed = 5;
            }

            Vector2f playerDirection = new(0, 0);

            if (GetPos().X < AppManager.GetScreenSize().X & Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                playerDirection += VectorConstants.Right;
                SetScale(new Vector2f(1, 1));
            }
            else if (GetPos().X > 0 & Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                playerDirection -= VectorConstants.Right;
                SetScale(new Vector2f(-1, 1));
            }

            if (GetPos().Y < AppManager.GetScreenSize().Y & Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                playerDirection += VectorConstants.Up;
            }
            else if (GetPos().Y > 0 & Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                playerDirection -= VectorConstants.Up;
            }

            bool isMoving = playerDirection != VectorConstants.Zero;

            if (isMoving)
            {
                lastMove = playerDirection;
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
                    new Shot(GetPos(), lastMove ?? new(GetScale().X, 0), 30, false).Bind();
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

            //movement before update
            base.Update();

        }

        public override void Collision(BaseSceneObject collided)
        {
            base.Collision(collided);


            if (collided is Tile tile && !tile.IsExit())
            {

                CollisionUtils.ApplyRectRigidCollision(this, (Tile)collided);
            }

        }

        public override void OnCollisionEnter(BaseSceneObject collided)
        {
            base.OnCollisionEnter(collided);
            if (collided is Tile tile && tile.IsExit())
            {
                AppManager.GetCurrentScene<MainScene>()?.SetPlayerCanExit(true);
            }
            else if (IsCollidedWithLivingEnemy(collided) || (collided is Shot shot && shot.IsFromEnemy()))
            {
                AppManager.ChangeScene(new GameOverScene());
            }
        }

        public override void OnCollisionExit(BaseSceneObject collided)
        {
            base.OnCollisionExit(collided);
            if (collided is Tile tile && tile.IsExit())
            {
                AppManager.GetCurrentScene<MainScene>()?.SetPlayerCanExit(false);
            }
        }



        public static Player? getInstance()
        {
            return instance;
        }

        private bool IsCollidedWithLivingEnemy(object collided)
        {
            //todo implement abstract enemy class
            return (collided is EnemySlow slow && !slow.Dead()) ||
                   (collided is EnemyFast fast && !fast.Dead()) ||
                   (collided is EnemyStatic stat && !stat.Dead());
        }
    }
}