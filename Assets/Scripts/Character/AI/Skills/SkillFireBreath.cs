using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public class SkillFireBreath : Skill
    {
        [field: SerializeField] public override string Name { get; protected set; } = "FireBreath";
        [field: SerializeField] public override float Range { get; set; } = 1f;

        [SerializeField] protected float stopDistance = 1f;
        public override void Activate(NavMeshAgent agent)
        {
            base.Activate(agent);

            agent.stoppingDistance = stopDistance;
            agent.enabled = false;
            targetDetector.Look = false;
        }
    }
}
