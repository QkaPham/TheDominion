using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateCoyoteTime : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "CoyoteTime";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0.1f;
    }
}
