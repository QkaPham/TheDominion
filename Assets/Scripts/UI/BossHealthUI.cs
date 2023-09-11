using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class BossHealthUI : MyMonoBehaviour
    {
        [SerializeField] protected Health health;
        [SerializeField] protected Image healthImage;

        public override void LoadComponent()
        {
            health = FindObjectOfType<PlayerHealth>();
            healthImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            health.HealthChanged += UpdateHealth;
        }

        private void OnDisable()
        {
            health.HealthChanged -= UpdateHealth;
        }

        private void UpdateHealth(float changeValue)
        {
            healthImage.fillAmount = health.CurrentHealth / health.MaxHealth;
        }
    }
}
