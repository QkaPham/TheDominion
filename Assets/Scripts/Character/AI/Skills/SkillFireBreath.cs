using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public class SkillFireBreath : Skill
    {
        [field: SerializeField] public override string Name { get; protected set; } = "FireBreath";
        [field: SerializeField] public override float Range { get; set; } =10f;
        [field: SerializeField] public float MinDistance { get; set; } = 5f;
        [field: SerializeField] public float Angle { get; set; } = 45f;
        public override bool CanUse => Mathf.Abs(targetDetector.SignedAngleToTarget()) <= Angle && targetDetector.DistanceToTarget <= Range && targetDetector.DistanceToTarget >= MinDistance;

        public override void Activate(AIStateMachine ai)
        {
            base.Activate(ai);

            ai.AiLook.Stop();
        }
    }
}
