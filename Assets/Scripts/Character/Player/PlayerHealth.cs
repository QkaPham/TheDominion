 using UnityEngine;

namespace Project3D
{
    public class PlayerHealth : Health
    {
        [SerializeField] private Stat endure;
        [HideInInspector] public override float MaxHealth => endure.Value;

        protected void Start() => endure.UpgradeEvent += OnStatUpgrade;

        protected void OnDestroy() => endure.UpgradeEvent -= OnStatUpgrade;

        private void OnStatUpgrade(int level) => HealFull();
    }
}
