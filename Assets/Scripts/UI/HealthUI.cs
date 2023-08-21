using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class HealthUI : MyMonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Image healthImage;

        public override void LoadComponent()
        {
            base.LoadComponent();
            healthImage = GetComponent<Image>();
        }

        private void Start()
        {
            health.HealthChanged += UpdateHealth;
        }

        private void OnDestroy()
        {
            health.HealthChanged -= UpdateHealth;
        }

        private void UpdateHealth(float changeValue)
        {
            healthImage.fillAmount = health.CurrentHealth / health.MaxHealth;
        }
    }
}
