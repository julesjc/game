namespace Game
{
    interface ICollider
    {
        void OnCollisionEnter(BaseObject collider);
        void OnCollisionExit(BaseObject collider);
        void Collision(BaseObject collider);
    }

}
