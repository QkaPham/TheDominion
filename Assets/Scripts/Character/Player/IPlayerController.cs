using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public interface IPlayerController
    {
        bool IsGrounded { get; }
        bool IsFalling { get; }

        void Move(Vector3 velocity);

        void SetTargetRotation(Vector3 direction);

        void Rotate();

        void UseGravity(bool isUse);

        void SnapToGround(float SnapSpeed);

        bool OnSteepSlope(out RaycastHit downwardHit);
    }
}