using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class SkillBite : Skill
    {
        [field: SerializeField] public override string Name { get; protected set; } = "Bite";
        [field: SerializeField] public override float Range { get; set; } = 1f;

        [SerializeField] protected float stopDistance = 1f;

        public override void Activate(AIStateMachine ai)
        {
            base.Activate(ai);

            ai.Agent.stoppingDistance = stopDistance;
            ai.Agent.enabled = false;
            aiLook.Stop();
        }
    }
}
