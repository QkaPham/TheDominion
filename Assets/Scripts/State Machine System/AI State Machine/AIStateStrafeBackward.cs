using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateStrafeBackward : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "StrafeBack";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float desireDistance = 3;
        [SerializeField] protected float moveSpeed = 1.5f;
        [SerializeField] private float rotationSpeed = 360f;

        public bool ReachDesireDistance => targetDetector.DistanceToTarget >= desireDistance;

        public override void Enter()
        {
            base.Enter();

            agent.updateRotation = false;
            agent.speed = moveSpeed;
        }

        public override void Exit()
        {
            base.Exit();

            agent.updateRotation = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            agent.SetDestination(stateMachine.transform.position - targetDetector.TargetDirection);
        }

        private void RotateToTarget()
        {
            var direction = targetDetector.TargetDirection;
            direction.y = 0;
            stateMachine.transform.rotation = Quaternion.RotateTowards(stateMachine.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), rotationSpeed * Time.deltaTime);

        }
    }
}