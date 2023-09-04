using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class HealthUI : MyMonoBehaviour
    {
        [SerializeField] protected Health health;
        [SerializeField] protected Image healthImage;

        public override void LoadComponent()
        {
            base.LoadComponent();      
            health = GetComponentInParent<Health>();
        }

        private void OnEnable()
        {
            health.HealthChanged += UpdateHealth;
        }

        private void OnDisable()
        {
            health.HealthChanged -= UpdateHealth;
        }

        protected virtual void UpdateHealth(float changeValue)
        {
            healthImage.fillAmount = health.CurrentHealth / health.MaxHealth;
        }
    }
}
