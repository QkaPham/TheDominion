using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class WeaponStorage : MonoBehaviour
    {
        public List<WeaponSO> ownedWeapons;

        public event Action<WeaponSO> NewWeaponAdd;

        public void Add(WeaponSO weapon)
        {
            ownedWeapons.Add(weapon);
            NewWeaponAdd?.Invoke(weapon);
        }
    }
}
