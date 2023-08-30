namespace Game
{
    interface ICollider
    {
        void OnCollisionEnter(BaseSceneObject collided);
        void OnCollisionExit(BaseSceneObject collided);
        void Collision(BaseSceneObject collided);
    }

}
