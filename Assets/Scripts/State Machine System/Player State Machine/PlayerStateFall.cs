using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateFall : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Fall";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float acceleration = 5f;

        public override void Enter()
        {
            base.Enter();

            currentVelocity = player.velocity;
            player.velocity.y = 0;
            player.UseGravity(true);
        }

        public override void Exit()
        {
            base.Exit();

            player.UseGravity(false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (input.Jump)
            {
                input.HasJumpBuffer = true;
            }

            currentVelocity = Vector3.MoveTowards(currentVelocity, moveSpeed * input.MoveAxesXZ, acceleration * Time.deltaTime);
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            player.MoveAndRotate(currentVelocity);
        }
    }
}
