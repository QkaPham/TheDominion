using System;
using UnityEngine;
using UnityEngine.Windows;

namespace Project3D
{
    [Serializable]
    public class PlayerStateJump : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Jump";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] private float jumpHeight = 2;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float acceleration = 5f;

        public bool IsUngrounded { get; private set; }
        private float jumpVelocity;

        public override void Initialize(Animator animator, PlayerController player, PlayerInput input, AnimationEventMeleeAttack animationEvent, PlayerStateMachine stateMachine)
        {
            base.Initialize(animator, player, input, animationEvent, stateMachine);
            jumpVelocity = Mathf.Sqrt(2 * 9.81f * jumpHeight);
        }

        public override void Enter()
        {
            base.Enter();

            currentVelocity = player.velocity;
            IsUngrounded = false;
            player.SetVerticalVelocity(jumpVelocity);
            player.UseGravity(true);
            input.HasJumpBuffer = false;
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
            if (!IsUngrounded) IsUngrounded = !player.IsGrounded;
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            // TODO: fix "feature" player jump higher when keep move into high slope
            player.MoveAndRotate(currentVelocity);
        }
    }
}