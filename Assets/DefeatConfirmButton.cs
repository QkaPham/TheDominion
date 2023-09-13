using UnityEngine;

namespace Project3D
{
    public class DefeatConfirmButton : ButtonBinder
    {
        [SerializeField] private UIManager uiManager;

        protected override void OnClick()
        {
            uiManager.ShowLast();
        }
    }
}
