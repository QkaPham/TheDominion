using System;
using UnityEngine;

namespace Project3D
{
    public class CheckPoint : MyMonoBehaviour, IInteractable
    {
        public bool CanInteract { get; set; } = true;
        public string Content => "Rest";

        public static event Action InteractEvent;
        public static CheckPoint lastInteractCheckPoint;
        [field: SerializeField] public Transform RevivePoint { get; private set; }

        private void Awake()
        {
            lastInteractCheckPoint = this;
        }

        public void Interact(PlayerController player)
        {
            player.GetComponent<Potion>().Refill();
            player.GetComponent<Health>().HealFull();
            lastInteractCheckPoint = this;
            InteractEvent?.Invoke();
        }
    }
}
