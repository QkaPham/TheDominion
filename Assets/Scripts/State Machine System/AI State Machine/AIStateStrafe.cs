using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateStrafe : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Strafe";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float moveSpeed = 1.5f;

        protected int randomSign = 1;

        public override void Enter()
        {
            base.Enter();

            randomSign = UnityEngine.Random.value < .5 ? 1 : -1;
            agent.updateRotation = false;
            animator.SetFloat(StateHash, randomSign);
        }

        public override void Exit()
        {
            base.Exit();

            agent.updateRotation = true;
            agent.speed = moveSpeed;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            stateMachine.transform.LookAt(targetDetector.Target);
            MoveAroundTarget();
        }

        protected void MoveAroundTarget()
        {
            var leftDirection = Vector3.Cross(targetDetector.TargetDirection, Vector3.up);
            agent.SetDestination(targetDetector.transform.position + (randomSign * leftDirection));
        }
    }
}