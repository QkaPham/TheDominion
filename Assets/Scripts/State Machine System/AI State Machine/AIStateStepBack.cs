using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateStepBack : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "StepBack";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        public float minDistance = 3;

        public override void Enter()
        {
            base.Enter();

            agent.updateRotation = false;
        }

        public override void Exit()
        {
            base.Exit();

            agent.updateRotation = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            stateMachine.transform.LookAt(targetDetector.Target);
            agent.SetDestination(stateMachine.transform.position - targetDetector.TargetDirection);
        }
    }
}