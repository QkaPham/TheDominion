using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateRun : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Run";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float runSpeed = 3f;
        [SerializeField] protected float snapSpeed = -10f;

        RaycastHit downwardHit;
        public bool IsOnSteepSlope { get; protected set; }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            IsOnSteepSlope = player.OnSteepSlope(out downwardHit, 0.2f);
            player.MoveAndRotate(runSpeed * Mathf.Sin(Vector3.Angle(input.MoveAxesXZ, downwardHit.normal) * Mathf.Deg2Rad) * input.MoveAxesXZ);
            player.SnapToGround(snapSpeed);
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.visualEffects.Play(StateName);
        }

        public override void Exit()
        {
            base.Exit();

            stateMachine.visualEffects.Stop(StateName);
        }
    }
}