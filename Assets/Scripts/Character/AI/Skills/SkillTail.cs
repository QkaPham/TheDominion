using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class SkillTail : Skill
    {
        [field: SerializeField] public override string Name { get; protected set; } = "Tail";
        [field: SerializeField] public override float Range { get; set; } = 1f;

        public override bool CanUse => Mathf.Abs(targetDetector.SignedAngleToTarget()) <= 120f && targetDetector.DistanceToTarget <= Range;

        public override void Activate(AIStateMachine ai)
        {
            base.Activate(ai);

            ai.RootMotionAgent.Apply(true, 1, 1f + Mathf.Abs(targetDetector.SignedAngleToTarget()) / 360f);
            ai.Animator.SetFloat(Hash, targetDetector.SignedAngleToTarget() > 0 ? -1 : 1);
            ai.AiLook.Stop();
        }
    }
}
