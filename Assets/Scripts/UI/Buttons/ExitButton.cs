using UnityEngine;

namespace Project3D
{
    public class ExitButton : ButtonBinder
    {
        protected override void OnClick()
        {
            Application.Quit();
        }
    }
}
