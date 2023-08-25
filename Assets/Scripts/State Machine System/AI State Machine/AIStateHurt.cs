using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateHurt : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Hurt";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0f;

        [SerializeField] protected float DelayAttackTime = 1.5f;

        public override bool HasTransitionRequest() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(StateName);

        public override void Enter()
        {
            base.Enter();

            if (!targetDetector.HasTarget())
            {
                ai.StopAttackFor(DelayAttackTime);
            }
            targetDetector.Look = false;
            agent.enabled = false;
        }

        public override void Exit()
        {
            base.Exit();

            targetDetector.Look = true;
            agent.enabled = true;
        }
    }
}