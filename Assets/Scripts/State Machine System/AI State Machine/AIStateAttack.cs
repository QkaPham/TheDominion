using System;
using UnityEngine;
using UnityEngine.AI;

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

            ai.StopAttackFor(attackCoolDown);
        }
    }
}