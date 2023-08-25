using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateDefeat : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Defeat";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0f;

        public override void Enter()
        {
            base.Enter();

            targetDetector.Look = false;
            agent.enabled = false;
        }
    }
}