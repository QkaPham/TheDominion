using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class UpgradeView : BaseView
    {
        [SerializeField] protected CameraSwitcher cameraSwitcher;

        protected override void PreShow()
        {
            base.PreShow();
            cameraSwitcher.SwitchCamera(cameraSwitcher.UpgradeCamera);
        }

        protected override void PreHide()
        {
            base.PreHide();
            cameraSwitcher.SwitchCamera(cameraSwitcher.TopdownCamera);
        }
    }
}
