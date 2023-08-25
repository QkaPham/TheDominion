using UnityEngine;

namespace Project3D
{
    public class Interactor : MyMonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerController player;
        [SerializeField] private InteractUI interactUI;

        private IInteractable interactable;

        public override void LoadComponent()
        {
            input = GetComponent<PlayerInput>();
            player = GetComponent<PlayerController>();
            interactUI = GetComponentInChildren<InteractUI>();
        }

        private void Update()
        {
            if (input.Interact && interactable != null)
            {
                interactable.Interact(player);
                interactUI.Hide();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                this.interactable = interactable;
                interactUI.Show(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                if (this.interactable == interactable)
                {
                    this.interactable = null;
                    interactUI.Hide();
                }
            }
        }
    }
}
