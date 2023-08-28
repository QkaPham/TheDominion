using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateFindAttacker : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "FindAttacker";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float maxTime = 10f;
        [SerializeField] protected float delayAttackTime = 1f;

        public bool TimeOut => StateDuration >= maxTime;

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            targetDetector.GetTargetInRadius();
        }
    }
}