using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class PlayerHealthUI : HealthUI
    {
        [SerializeField] private RectTransform healthBarRect;
        [SerializeField] private float widthPerHealth = 4f;
        [SerializeField] private Color high;
        [SerializeField] private Color low;

        public override void LoadComponent()
        {
            health = FindObjectOfType<PlayerHealth>();
            healthImage = transform.Find("Fill").GetComponent<Image>();
            healthBarRect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            healthBarRect.sizeDelta = new Vector2(widthPerHealth * health.MaxHealth, healthBarRect.sizeDelta.y);
        }

        protected override void UpdateHealth(float changeValue)
        {
            base.UpdateHealth(changeValue);
            healthBarRect.sizeDelta = new Vector2(widthPerHealth * health.MaxHealth, healthBarRect.sizeDelta.y);
            healthImage.color = healthImage.fillAmount >= 0.3 ? high : low;
        }
    }
}
