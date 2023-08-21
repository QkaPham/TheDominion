using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public enum WeaponPosition
    {
        None =-1,
        LeftHand,
        RightHand,
        Back,
        //Mouth
    }

    public class WeaponPositionMark : MyMonoBehaviour
    {
        [field: SerializeField] public WeaponPosition Location { get; private set; }
        private GameObject weapon;

        public void Initialize(GameObject weaponPrefab)
        {
            weapon = Instantiate(weaponPrefab, transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            Unload();
        }

        public void Load() => weapon.SetActive(true);

        public void Unload() => weapon.SetActive(false);

        public void Unequip() => Destroy(weapon);
    }
}