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
            stateMachine.health.HealFull();
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
}