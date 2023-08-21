using System;
using UnityEngine;

namespace Project3D
{
    [CreateAssetMenu(menuName = "Data/Stat", fileName = "")]
    public class Stat : ScriptableObject
    {
        private const int MaxLevel = 5;
        private readonly int[] UpgradePrices = { 200, 300, 450, 700, 1000 };
        
        public StatID id;
        public event Action<int> UpgradeEvent;
        [SerializeField] private float Base = 10f;
        [SerializeField] private float Increment = 0.2f;
        public int Level { get; private set; } = 0;

        public float Value => Base * (1 + Level * Increment);
        public bool IsMax() => Level >= MaxLevel;
        public void Upgrade()
        {
            Level++;
            UpgradeEvent?.Invoke(Level);
        }
        public int UpgradePrice() => UpgradePrices[Level];
    }
}