using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class PlayerHealthUI : HealthUI
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float widthPerHealth = 4f;

        public override void LoadComponent()
        {
            health = FindObjectOfType<PlayerHealth>();
            healthImage = GetComponent<Image>();
            rectTransform = healthImage.GetComponent<RectTransform>();
        }

        private void Start()
        {
            rectTransform.sizeDelta = new Vector2(widthPerHealth * health.MaxHealth, rectTransform.sizeDelta.y);
        }

        protected override void UpdateHealth(float changeValue)
        {
            base.UpdateHealth(changeValue);
            rectTransform.sizeDelta = new Vector2(widthPerHealth * health.MaxHealth, rectTransform.sizeDelta.y);
        }
    }
}
