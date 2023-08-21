using System;

namespace Project3D
{
    public class CheckPoint : MyMonoBehaviour, IInteractable
    {
        public bool CanInteract { get; set; } = true;
        public  string Content => "Check Point";

        public static event Action InteractEvent;

        public void Interact(PlayerController player)
        {
            player.Health.HealFull();
            InteractEvent?.Invoke();
        }
    }
}
