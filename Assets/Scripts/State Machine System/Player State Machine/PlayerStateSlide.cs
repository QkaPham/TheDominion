using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateSlide : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Slide";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;

        [SerializeField] protected float slideSpeed = 3f;


        protected RaycastHit downwardHit;
        public bool IsOnSteepSlope { get; protected set; }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
            IsOnSteepSlope = player.OnSteepSlope(out downwardHit);
            player.velocity = Vector3.ProjectOnPlane(Vector3.down, downwardHit.normal).normalized * slideSpeed;
        }
    }
}