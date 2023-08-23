﻿using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public class AIStateChase : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Chase";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float chaseSpeed = 3f;
        [SerializeField] protected float startAttackDistance = 1.5f;
        [SerializeField] protected float maxTime = 3f;

        public bool ReachAttackRange() => targetDetector.DistanceToTarget <= startAttackDistance;
        public bool TimeOut => StateDuration >= maxTime;

        public override void Enter()
        {
            base.Enter();

            agent.speed = chaseSpeed;
        }

        public override void LogicUpdate()
        {
            agent.SetDestination(targetDetector.Target.position);
        }
    }
}