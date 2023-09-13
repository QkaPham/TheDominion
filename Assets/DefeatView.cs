using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class DefeatView : BaseView
    {
        [SerializeField] PlayerStateMachine player;

        protected override void PreHide()
        {
            base.PreHide();
            player.Revive(CheckPoint.lastInteractCheckPoint.RevivePoint.position);
        }
    }
}
