using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public class SkillSpecial : Skill
    {
        [field: SerializeField] public override string Name { get; protected set; } = "Special";
        [field: SerializeField] public override float Range { get; set; } = 5f;

        public override bool CanUse => Mathf.Abs(targetDetector.SignedAngleToTarget()) >= 90f && targetDetector.DistanceToTarget <= Range;
        public override void Activate(AIStateMachine ai)
        {
            base.Activate(ai);

            ai.RootMotionAgent.Apply(true, 1, 1f - targetDetector.SignedAngleToTarget() / 360f);
            ai.AiLook.Stop();
        }
    }
}
