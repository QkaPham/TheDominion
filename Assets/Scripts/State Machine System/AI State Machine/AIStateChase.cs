using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateChase : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Chase";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float chaseSpeed = 3f;

        public override void Enter()
        {
            base.Enter();

            agent.speed = chaseSpeed;
        }

        public override void LogicUpdate()
        {
            agent.SetDestination(targetDetector.Destination);
        }
    }

    [Serializable]
    public class AIStateChaseRootMotion : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Chase";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float moveSpeed = 1f;
        [SerializeField] protected float rotationSpeed = 1f;

        public override void Enter()
        {
            base.Enter();

            rootMotionAgent.Apply(true, moveSpeed, rotationSpeed);
        }

        public override void LogicUpdate()
        {
            base .LogicUpdate();

            animator.SetFloat(StateHash, targetDetector.SignedAngleToTarget() / 90f);
        }

        public override void Exit()
        {
            base.Exit();

            rootMotionAgent.Apply(false);
        }
    }
}