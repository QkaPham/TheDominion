using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class WeaponPanel : MyMonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private CanvasGroup canvasGroup;
       
        private bool isActive;

        public override void LoadComponent()
        {
            base.LoadComponent();
            input = FindAnyObjectByType<PlayerInput>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
        }

        private void Update()
        {
            if (input.Pause)
            {
                Show();
            }

            if (input.Cancel)
            {
                Hide();
            }
        }

        private void Show()
        {
            isActive = true;
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            input.SwitchControls(ControlsID.UI);
        }

        private void Hide()
        {
            isActive = false;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            input.SwitchControls(ControlsID.Player);
        }

        private void UpdateWeapon(WeaponSO weapon)
        {

        }
    }

    public class BasePanel: MyMonoBehaviour
    {

    }
}
