﻿using System;
using UnityEngine;

namespace Project3D
{
    [CreateAssetMenu(menuName = "Data/Stat", fileName = "")]
    public class Stat : ScriptableObject
    {
        public static int MaxLevel = 5;
        private readonly int[] UpgradePrices = { 200, 300, 450, 700, 1000 };

        public StatID id;
        public event Action<int> OnLevelChange;

        [SerializeField] private float Base = 10f;
        [SerializeField] private float Increment = 0.2f;
        [SerializeField] private int level = 0;

        public int Level
        {
            get => level;
            set
            {
                level = value;
                OnLevelChange?.Invoke(level);
            }
        }

        public float Value => Base * (1 + Level * Increment);
        public bool IsMax() => Level >= MaxLevel;
        public void Upgrade() => Level++;
        public int UpgradePrice() => UpgradePrices[Level];
    }
}