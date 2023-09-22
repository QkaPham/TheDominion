using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateRoll : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Roll";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.05f;

        [SerializeField] private float speed = 2f;
        public override bool HasRequestTransition() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(StateName) && !animator.IsInTransition(0);

        public override void Enter()
        {
            base.Enter();
            player.ApplyRootMotion(true, false, speed, 1f);
        }

        public override void Exit()
        {
            base.Exit();
            player.ApplyRootMotion(false);
        }
    }
}