using UnityEngine;

namespace Project3D
{
    public class GameOverPanel : MyMonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CanvasGroup canvasGroup;

        public override void LoadComponent()
        {
            base.LoadComponent();
            playerController = FindAnyObjectByType<PlayerController>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
        }

        private void OnEnable()
        {
            playerController.DeathEvent += Show;
        }

        private void OnDisable()
        {
            playerController.DeathEvent -= Show;
        }

        private void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        private void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
