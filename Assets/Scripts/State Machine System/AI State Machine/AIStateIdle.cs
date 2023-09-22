using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateIdle : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Idle";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0.1f;

        public override void Enter()
        {
            base.Enter();

            agent.enabled = false;
        }

        public override void Exit()
        {
            base.Exit();

            agent.enabled = true;
        }


        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            if (!targetDetector.HasTarget())
            {
                targetDetector.GetTargetForward();
            }
        }
    }

    [Serializable]
    public class AIStateIdleRootMotion : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Idle";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0.1f;
        [SerializeField] protected float moveSpeed = 1f;
        [SerializeField] protected float rotationSpeed = 1f;

        private float smoothAngle = 0f;
        public override void Enter()
        {
            base.Enter();

            smoothAngle = 0f;
            rootMotionAgent.Apply(true, moveSpeed, rotationSpeed);
        }

        public override void Exit()
        {
            base.Exit();

            rootMotionAgent.Apply(false);
        }


        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            if (!targetDetector.HasTarget())
            {
                targetDetector.GetTargetForward();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            float targetAngle = targetDetector.SignedAngleToTarget();
            smoothAngle = Mathf.MoveTowards(smoothAngle, targetAngle, 90 * Time.deltaTime);
            animator.SetFloat(StateName, smoothAngle);
        }
    }
}