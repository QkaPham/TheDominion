using UnityEngine;

namespace Project3D
{
    public enum WeaponPosition
    {
        None =-1,
        LeftHand,
        RightHand,
        Back,
    }

    public class WeaponPositionMark : MyMonoBehaviour
    {
        [field: SerializeField] public WeaponPosition Position { get; private set; }
        private GameObject weapon;

        public void Initialize(GameObject weaponPrefab)
        {
            weapon = Instantiate(weaponPrefab, transform);
            weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            Unload();
        }

        public void Load()
        {
            if (weapon == null) return;
            weapon.SetActive(true);
        }

        public void Unload()
        {
            if (weapon == null) return;
            weapon.SetActive(false);
        }

        public void Unequip() => Destroy(weapon);
    }
}