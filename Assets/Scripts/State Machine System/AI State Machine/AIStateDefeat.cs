using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateDefeat : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Defeat";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0f;

        public override void Enter()
        {
            base.Enter();

            agent.enabled = false;
        }
    }
}