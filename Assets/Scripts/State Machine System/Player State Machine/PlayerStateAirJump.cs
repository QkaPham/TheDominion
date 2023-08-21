using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateAirJump : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "AirJump";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] float jumpHeight = 1f;
        [SerializeField] float moveSpeed = 3f;
        [SerializeField] float acceleration = 5f;

        private float jumpVelocity;

        public override void Initialize(Animator animator, PlayerController player, PlayerInput input, PlayerAnimationEvent animationEvent, PlayerStateMachine stateMachine)
        {
            base.Initialize(animator, player, input, animationEvent, stateMachine);
            jumpVelocity = Mathf.Sqrt(2 * 9.81f * jumpHeight);
        }

        public override void Enter()
        {
            base.Enter();

            currentVelocity = player.velocity;
            player.CanAirJump = false;
            player.SetVerticalVelocity(jumpVelocity);
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

            currentVelocity = Vector3.MoveTowards(currentVelocity, moveSpeed * input.MoveAxesXZ, acceleration * Time.deltaTime);
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            player.MoveAndRotate(currentVelocity);
        }
    }
}
