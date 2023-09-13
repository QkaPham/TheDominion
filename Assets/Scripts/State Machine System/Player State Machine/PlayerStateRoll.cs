using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateRoll : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Roll";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.05f;

        [SerializeField] private float speed = 6f;
        public override bool HasRequestTransition() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(StateName) && !animator.IsInTransition(0);

        private Vector3 velocity;

        public override void Enter()
        {
            base.Enter();
            player.velocity = Vector3.zero;
            velocity = speed * (input.MoveAxesXZ == Vector3.zero ? player.transform.forward : input.MoveAxesXZ);
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            velocity.y = player.IsGrounded ? 0f : velocity.y - 10f * Time.deltaTime;
            player.MoveAndRotate(velocity);
        }
    }
}