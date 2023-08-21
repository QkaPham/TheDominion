using UnityEngine;

namespace Project3D
{
    [CreateAssetMenu(menuName = "Data/Weapon", fileName = "New Weapon")]
    public class WeaponSO : ScriptableObject
    {
        public string WeaponName;

        public GameObject Model;

        public Transform HitBoxsPrefab;
        
        public AnimatorOverrideController AnimatorOverride;
        
        public int HitNumber;
        
        public float DamageMultiplier;
        
        public WeaponHanding Handing;
    }

    public enum WeaponHanding
    {
        Single,
        Dual
    }
}
