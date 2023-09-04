using System;
using UnityEngine;

namespace Project3D
{
    public class Health : MyMonoBehaviour
    {
        [field: SerializeField] public virtual float MaxHealth { get; set; } = 10;

        protected float currentHealth;
        public float CurrentHealth
        {
            get => currentHealth;
            protected set
            {
                currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            }
        }

        public float LoseHealth => MaxHealth - CurrentHealth;

        public event System.Action Defeat;
        public event System.Action<float> HealthChanged;
        public bool IsDead => CurrentHealth <= 0;

        protected void Awake() => HealFull();

        public void TakeDamage(float damage)
        {
            if (IsDead) return;
            CurrentHealth -= damage;
            HealthChanged?.Invoke(-damage);

            if (IsDead) Defeat?.Invoke();
        }

        public void Heal(float heal)
        {
            CurrentHealth += heal;
            HealthChanged?.Invoke(heal);
        }

        public void HealFull()
        {
            var lostHealth = LoseHealth;
            CurrentHealth = MaxHealth;
            HealthChanged?.Invoke(lostHealth);
        }
    }
}
