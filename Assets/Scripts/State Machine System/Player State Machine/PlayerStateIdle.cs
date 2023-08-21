using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateIdle : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Idle";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        public bool IsOnSteepSlope => player.OnSteepSlope(out _);

        public override void Enter()
        {
            base.Enter();
            player.velocity = Vector3.down;
        }
    }
}
