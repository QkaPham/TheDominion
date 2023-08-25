using UnityEngine;
using UnityEngine.InputSystem;

namespace Project3D
{
    public class CheatConsole : MyMonoBehaviour
    {
        [SerializeField] PlayerInput input;
        [SerializeField] CanvasGroup canvasGroup;
        bool isShown;

        private void Start()
        {
            input = FindAnyObjectByType<PlayerInput>();
            canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }

        void Update()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                isShown = !isShown;
                if (isShown)
                    Show();
                else
                    Hide();
            }
        }

        private void Show()
        {
            isShown = true;
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            input.SwitchControls(ControlsID.UI);
        }


        private void Hide()
        {
            isShown = false;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            input.SwitchControls(ControlsID.Player);
        }
    }
}
