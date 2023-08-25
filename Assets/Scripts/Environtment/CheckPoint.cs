using System;

namespace Project3D
{
    public class CheckPoint : MyMonoBehaviour, IInteractable
    {
        public bool CanInteract { get; set; } = true;
        public string Content => "Rest";

        public static event Action InteractEvent;

        public void Interact(PlayerController player)
        {
            player.GetComponent<Potion>().Refill();
            player.GetComponent<Health>().HealFull();
            InteractEvent?.Invoke();
        }
    }
}
