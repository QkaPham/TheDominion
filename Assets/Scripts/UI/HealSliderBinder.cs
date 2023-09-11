using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class HealSliderBinder : MyMonoBehaviour
    {

        [SerializeField] private Health health;
        [SerializeField] private Slider healthImage;

        public override void LoadComponent()
        {
            base.LoadComponent();
            healthImage = GetComponent<Slider>();
            health = FindObjectOfType<PlayerController>().GetComponent<Health>();
        }

        private void Start()
        {
            health.HealthChanged += UpdateHealth;
        }

        private void OnDestroy()
        {
            health.HealthChanged -= UpdateHealth;
        }

        private void UpdateHealth(float diff)
        {
            healthImage.value = health.CurrentHealth / health.MaxHealth;
        }
    }
}
