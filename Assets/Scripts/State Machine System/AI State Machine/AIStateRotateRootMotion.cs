using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateRotateRootMotion : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Rotate";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = .1f;

        [SerializeField] protected float moveSpeed = 1f;
        [SerializeField] protected float rotationSpeed = 1f;

        public override bool HasTransitionRequest()
        {
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(StateName);
        }

        public override void Enter()
        {
            animator.SetFloat(StateHash, targetDetector.SignedAngleToTarget() > 0 ? 1 : -1);
            rootMotionAgent.Apply(true, moveSpeed, rotationSpeed);

            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            rootMotionAgent.Apply(false);
        }
    }
}