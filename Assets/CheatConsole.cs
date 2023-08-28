using UnityEngine;

namespace Project3D
{
    public class CheatConsole : MyMonoBehaviour
    {
        [SerializeField] PlayerInput input;
        [SerializeField] CanvasGroup canvasGroup;

        public override void LoadComponent()
        {
            base.LoadComponent();

            input = FindObjectOfType<PlayerInput>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
        }

        void Update()
        {
            if (input.Command)
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
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            input.SwitchControls(ControlsID.UI);
        }


        private void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            input.SwitchControls(ControlsID.Player);
        }
    }
}
