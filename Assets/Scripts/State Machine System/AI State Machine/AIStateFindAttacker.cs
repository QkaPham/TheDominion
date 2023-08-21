using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateFindAttacker : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "FindAttacker";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float findTime = 10f;
        [SerializeField] protected float stiffTime = 1f;
        [SerializeField] protected float delayAttackTime = 1f;

        public bool TimeOut => StateDuration >= findTime;
        public bool StiffTimeOut => StateDuration >= stiffTime;

        public override void Enter()
        {
            base.Enter();
            Debug.Log("enter ");
            ai.StopAttackFor(delayAttackTime);
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            targetDetector.GetTargetInRadius();
        }
    }
}