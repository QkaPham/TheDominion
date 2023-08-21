using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateLand : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Land";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] float stiffTime = 0.2f;
        public bool IsLandFinished => StateDuration < stiffTime;
        public bool IsOnSteepSlope => player.OnSteepSlope(out _);

        public override void Enter()
        {
            base.Enter();

            currentVelocity = player.velocity;
            player.velocity = Vector3.down;
            player.CanAirJump = true;
        }
    }
}