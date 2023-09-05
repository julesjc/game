
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Tile : RectColliderSprite
    {
        private bool isExit, isInfected;
        private int timeElapsed;
        private static Texture texture = new Texture("data/sprites/tree.png");
        private static Texture textureExit = new Texture("data/sprites/exit.png");
        private static Sound deathSound = AudioManager.LoadSound("data/sound/evolving.ogg");


        public Tile(bool isExit = false) : base()
        {
            SetTexture(texture);
            this.isExit = isExit;
        }

        public override void Update()
        {
            base.Update();
            if (isInfected)
            {
                timeElapsed++;

                Vector2f wave = VectorUtils.Wave(timeElapsed, 0.2f, 0.2f);

                SetScale(wave);

                if (timeElapsed >= 100)
                {

                    Die();
                    //beurk
                    EnemySlow infectedChild = new EnemySlow(GetPos());
                    AppManager.GetCurrentScene<MainScene>()?.AddToMap(infectedChild);
                    infectedChild.Bind();
                }
            }

        }

        public void SetToExit()
        {
            SetTexture(textureExit);
            isExit = true;
        }

        public bool IsExit()
        {
            return isExit;
        }

        public override void OnCollisionEnter(BaseSceneObject collided)
        {
            base.OnCollisionEnter(collided);
            if (collided is EnemyFast && !isExit && !isInfected)
            {
                GetSprite().Color = Color.Yellow;
                AudioManager.PlaySound(deathSound);
                isInfected = true;
            }
        }

        public override void Unload()
        {
            base.Unload();
        }
    }
}