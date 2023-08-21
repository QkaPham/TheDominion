using UnityEngine;

namespace Project3D
{
    public class ExitButton : ButtonBinder
    {
        protected override void OnClick()
        {
            Debug.Log("Exit Application");
        }
    }
}
