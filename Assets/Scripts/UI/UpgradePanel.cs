using UnityEngine;

namespace Project3D
{
    public class UpgradePanel : MyMonoBehaviour
    {
        [SerializeField] private PlayerInput input;

        [SerializeField] private CanvasGroup canvasGroup;

        public override void LoadComponent()
        {
            base.LoadComponent();
            input = FindObjectOfType<PlayerInput>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            CheckPoint.InteractEvent += Show;
        }

        private void OnDisable()
        {
            CheckPoint.InteractEvent -= Show;
        }

        private void Start()
        {
            Hide();
        }

        private void Update()
        {
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
