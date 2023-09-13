using System;
using UnityEngine;

namespace Project3D
{
    [CreateAssetMenu(menuName = "Data/Weapon", fileName = "New Weapon")]
    public class WeaponSO : ScriptableObject
    {
        public string WeaponName;

        public WeaponModel[] WeaponModels;

        public Transform HitBoxsPrefab;

        public AnimatorOverrideController AnimatorOverride;

        public int HitNumber;

        public float DamageMultiplier;

        public WeaponHanding Handing;
    }

    [Serializable]
    public class WeaponModel
    {
        public GameObject GaneObject;
        public WeaponPosition Position;
    }

    public enum WeaponHanding
    {
        Single,
        Dual
    }
}
