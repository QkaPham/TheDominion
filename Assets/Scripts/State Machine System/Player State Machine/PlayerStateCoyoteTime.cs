using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateCoyoteTime : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "CoyoteTime";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] float runSpeed = 3f;
        [SerializeField] float coyoteTime = 0.1f;

        public override void Enter()
        {
            base.Enter();

            player.UseGravity(false);
            
        }

        public override void Exit()
        {
            base.Exit();

            player.UseGravity(true);
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            player.Move(runSpeed * input.MoveAxesXZ);
            player.velocity.y = 0f;
        }
    }
}
