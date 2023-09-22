using System;
using UnityEngine;

namespace Project3D
{
    public class PauseView : BaseView
    {
        protected override void PreShow()
        {
            base.PreShow();

            Time.timeScale = 0f;
        }
    }
}
