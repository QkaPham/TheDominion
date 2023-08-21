using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateAwaitCooldown : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "AwaitCooldown";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        private bool isMoveLeft;

        public override void Enter()
        {
            base.Enter();

            isMoveLeft = UnityEngine.Random.Range(0, 2) == 0;
            agent.updateRotation = false;
            animator.SetFloat("StateName", isMoveLeft ? 0 : 1);
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
            MoveAroundTarget(isMoveLeft);
        }

        public void MoveAroundTarget(bool isMoveLeft)
        {
            var leftDirection = Vector3.Cross(targetDetector.TargetDirection, Vector3.up);
            agent.SetDestination(targetDetector.transform.position + (isMoveLeft ? leftDirection : -leftDirection));
        }
    }
}