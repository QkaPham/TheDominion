 using UnityEngine;

namespace Project3D
{
    public class PlayerHealth : Health
    {
        [SerializeField] private Stat endure;
         public override float MaxHealth => endure.Value;

        protected void Start() => endure.OnLevelChange += OnStatUpgrade;

        protected void OnDestroy() => endure.OnLevelChange -= OnStatUpgrade;

        private void OnStatUpgrade(int level) => HealFull();
    }
}
