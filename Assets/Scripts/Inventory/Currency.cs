using System;
using UnityEngine;

namespace Project3D
{
    public class Currency : MyMonoBehaviour
    {
        [SerializeField] private int gold;
        public int Gold
        {
            get => gold;
            private set
            {
                gold = value;
                GoldChange?.Invoke(gold);
            }
        }

        public event Action<int> GoldChange;

        private void Awake()
        {
            Gold = 0;
        }

        public void Add(int value)
        {
            Gold += value;
        }

        public bool Use(int value)
        {
            if (value > Gold) return false;

            Gold -= value;
            return true;
        }
    }
}
