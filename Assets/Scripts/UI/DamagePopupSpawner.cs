using UnityEngine;

namespace Project3D
{
    public class DamagePopupSpawner : MyMonoBehaviour
    {
        [SerializeField] private Health health;

        [SerializeField] private DamagePopup damagePopupPrefab;
        [SerializeField] private float randomPositonRadius = 0.1f;
        public override void LoadComponent()
        {
            base.LoadComponent();
            health = GetComponentInParent<Health>();
        }

        private void Start() => health.HealthChanged += CreateDamagePopup;

        private void OnDestroy() => health.HealthChanged -= CreateDamagePopup;

        private void CreateDamagePopup(float damage)
        {
            if (damage == 0) return;
            Instantiate(damagePopupPrefab, randomPositonRadius * Random.insideUnitSphere + transform.position, transform.rotation).Initialize(Mathf.Abs(damage));
        }
    }
}
