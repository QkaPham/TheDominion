using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateDash : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Dash";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.05f;

        [SerializeField] private float dashTime = 0.2f;
        [SerializeField] private float dashDistance = 1f;
        public override bool HasRequestTransition() => StateDuration > dashTime;

        public override void Enter()
        {
            base.Enter();

            player.Move(dashDistance / dashTime * (input.MoveAxesXZ == Vector3.zero ? player.transform.forward : input.MoveAxesXZ));
        }
    }
}