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
    }
}