using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public class SkillBite : Skill
    {
        [field: SerializeField] public override string Name { get;protected set; } = "Bite";
        [field: SerializeField] public override float SkillRange { get; set; } = 1f;

        [SerializeField] protected float stopDistance = 1f;
        public override void Activate(NavMeshAgent agent)
        {
            base.Activate(agent);

            agent.stoppingDistance = stopDistance;
            agent.enabled = false;
        }
    }
}
