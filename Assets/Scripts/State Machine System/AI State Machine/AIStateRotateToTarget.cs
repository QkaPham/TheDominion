using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateRotateToTarget : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Chase";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0f;

        public override bool HasTransitionRequest()
        {
            return Mathf.Abs(targetDetector.SignedAngleToTarget()) <= 45f;
        }

        public override void Enter()
        {
            base.Enter();
            animator.GetComponent<RootMotionAgent>().Apply = true;
        }
        private float current;
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            var target = Mathf.Round(targetDetector.SignedAngleToTarget() / 45f);
            current = Mathf.MoveTowards(current, target, Time.deltaTime * 3);
            animator.SetFloat(StateHash, current);
        }

        public override void Exit()
        {
            base.Exit();

            animator.GetComponent<RootMotionAgent>().Apply = false;
        }
    }
}