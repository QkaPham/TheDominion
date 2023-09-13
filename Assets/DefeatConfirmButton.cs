using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class DefeatConfirmButton : ButtonBinder
    {
        [SerializeField] private PlayerStateMachine player;

        protected override void OnClick()
        {
            //Revive and move player to last check point

            player.Revive();
        }
    }
}
