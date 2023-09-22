using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public class SkillPound : Skill
    {
        [field: SerializeField] public override string Name { get; protected set; } = "Pound";
        [field: SerializeField] public override float Range { get; set; } = 3f;

        [SerializeField] protected float minDistanceToUse = 2f;
        [SerializeField] protected float stopDistance = 1f;
        [SerializeField] protected float moveTime = 0.5f;
        public override bool CanUse => targetDetector.DistanceToDestination > minDistanceToUse;

        public override void Activate(AIStateMachine ai)
        {
            base.Activate(ai);

            ai.Agent.speed = targetDetector.DistanceToDestination / moveTime;
            ai.Agent.stoppingDistance = stopDistance;
            ai.Agent.SetDestination(targetDetector.Target.position);
        }
    }
}
