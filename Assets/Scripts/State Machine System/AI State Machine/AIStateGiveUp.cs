using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateGiveUp : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "GiveUp";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0f;

        [SerializeField] protected float moveSpeed = 4f;

        public override void Enter()
        {
            base.Enter();

            agent.enabled = true;
            agent.speed = moveSpeed;
            targetDetector.GiveUp();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            agent.SetDestination(stateMachine.startPoint);
        }
    }
}