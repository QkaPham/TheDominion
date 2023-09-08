using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace Project3D
{
    [Serializable]
    public class SkillSpecial : Skill
    {
        [field: SerializeField] public override string Name { get; protected set; } = "Special";
        [field: SerializeField] public override float Range { get; set; } = 5f;
        public Rig headRig;

        public override void Activate(NavMeshAgent agent)
        {
            base.Activate(agent);

            agent.enabled = false;
            targetDetector.Look = false;
        }
    }
}
