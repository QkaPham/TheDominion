using UnityEngine;

namespace Project3D
{
    public class InteractUI : MyMonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private InteractTextBinder text;

        public override void LoadComponent()
        {
            base.LoadComponent();
            canvasGroup = GetComponent<CanvasGroup>();
            text = GetComponentInChildren<InteractTextBinder>();
        }

        private void Start() => Hide();

        public void Show(IInteractable interactable)
        {
            text.SetText(interactable.Content);
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
