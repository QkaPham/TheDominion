﻿using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateGiveUp : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "GiveUp";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0f;

        [SerializeField] protected float moveSpeed = 4f;

        public override void Enter()
        {
            base.Enter();

            agent.speed = moveSpeed;
            targetDetector.GiveUp();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            agent.SetDestination(stateMachine.startPoint.position);
        }
    }
}