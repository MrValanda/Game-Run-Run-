    using UnityEngine;

    public interface IMovement
    {
    bool UseGravity{get;set;}
        void Movement(Vector3 direction,float speed=0);
        void Jump();
        void Move(Vector3 direction);
        bool OnGround();
    }