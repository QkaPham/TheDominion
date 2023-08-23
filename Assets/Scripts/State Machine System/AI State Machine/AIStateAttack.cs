using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateAttack : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Attack";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0f;

        [SerializeField] private float attackCoolDown = 5f;

        [SerializeField] private float startChasingDistance = 2f;

        protected override int StateHash => stateMachine.aiSkill.nextSkill.hash;

        public override void Enter()
        {
            base.Enter();

            Debug.Log(stateMachine.aiSkill.nextSkill.name);

            stateMachine.transform.LookAt(targetDetector.Target.transform);
            ai.StopAttackFor(attackCoolDown);
            stateMachine.UseSkill();
            
        }

        public override void Exit()
        {
            base.Exit();

            agent.stoppingDistance = 0f;
            agent.enabled = true;
        }
    }
}