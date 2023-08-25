using UnityEngine;

namespace Project3D
{
    public class PlayerControllerRB : MyMonoBehaviour, IPlayerController
    {
        public bool IsGrounded => throw new System.NotImplementedException();

        public bool IsFalling => throw new System.NotImplementedException();

        public void Move(Vector3 velocity)
        {
            throw new System.NotImplementedException();
        }

        public bool OnSteepSlope(out RaycastHit downwardHit)
        {
            throw new System.NotImplementedException();
        }

        public void Rotate()
        {
            throw new System.NotImplementedException();
        }

        public void SetTargetRotation(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }

        public void SnapToGround(float SnapSpeed)
        {
            throw new System.NotImplementedException();
        }

        public void UseGravity(bool isUse)
        {
            throw new System.NotImplementedException();
        }
    }
}
