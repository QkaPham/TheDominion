using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class WinComfirmButton : ButtonBinder
    {
        [SerializeField] private UIManager uiManager;

        protected override void OnClick()
        {
            uiManager.ShowLast();
        }
    }
}
