using UnityEngine;

namespace Project3D
{
    public class OptionsButton : ButtonBinder
    {
        protected override void OnClick()
        {
            Debug.Log("Open Options Panel");
        }
    }
}
