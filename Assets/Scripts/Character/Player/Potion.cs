using System;
using UnityEngine;

namespace Project3D
{
    public class Potion : MyMonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private Health health;

        [SerializeField] private float recovery = 40;
        [SerializeField] private int maxQuantity = 3;
        public int MaxQuantity => maxQuantity;
        private int quantity = 3;

        public int Quantity
        {
            get => quantity;
            private set
            {
                quantity = value;
                QuantityChanged?.Invoke();
            }
        }

        public event Action QuantityChanged;

        public override void LoadComponent()
        {
            base.LoadComponent();
            input = GetComponent<PlayerInput>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            Refill();
        }

        private void Update()
        {
            if (input.UsePotion)
            {
                UsePotion();
            }
        }

        private void UsePotion()
        {
            if (Quantity <= 0) return;

            Quantity--;
            health.Heal(recovery);
        }

        public void Refill()
        {
            Quantity = maxQuantity;
        }
    }
}
