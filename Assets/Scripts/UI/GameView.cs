using System;
using System.Collections;
using UnityEngine;

namespace Project3D
{
    public class GameView : BaseView
    {
        protected override void Start()
        {
            StartCoroutine(PlayAnimation(true, 0, null));
        }

        protected override void PostShow()
        {
            input.EnablePlayerInput(true);
        }
    }
}
