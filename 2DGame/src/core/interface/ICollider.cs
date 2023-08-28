namespace Game
{
    interface ICollider
    {
        void OnCollisionEnter(BaseObject collided);
        void OnCollisionExit(BaseObject collided);
        void Collision(BaseObject collided);
    }

}
