using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class SkillStump : Skill
    {
        [field: SerializeField] public override string Name { get; protected set; } = "Stump";
        [field: SerializeField] public override float Range { get; set; } = 1f;
        [field: SerializeField] public float Angle { get; set; } = 60f;

        public override bool CanUse => Mathf.Abs(targetDetector.SignedAngleToTarget()) <= Angle && targetDetector.DistanceToTarget <= Range;

        public override void Activate(AIStateMachine ai)
        {
            base.Activate(ai);

            ai.RootMotionAgent.IsApply = true;
            ai.AiLook.Stop();
        }
    }
}
