using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateAttack : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Attack";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0f;

        [SerializeField] private float attackCoolDown = 5f;

        [SerializeField] private float startChasingDistance = 2f;


        public override void Enter()
        {
            base.Enter();

            agent.enabled = false;
            stateMachine.transform.LookAt(targetDetector.transform);
            ai.StopAttackFor(attackCoolDown);
        }

        public override void Exit()
        {
            base.Exit();

            agent.enabled = true;
        }
    }
}