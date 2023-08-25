using UnityEngine;

namespace Project3D
{
    public class Potion : MyMonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private Health health;

        [SerializeField] private float recovery = 40;
        [SerializeField] private int maxQuantity = 3;
         private int quantity = 3;

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
            if (quantity <= 0) return;

            quantity--;
            health.Heal(recovery);
        }

        public void Refill()
        {
            quantity = maxQuantity;
        }
    }
}
