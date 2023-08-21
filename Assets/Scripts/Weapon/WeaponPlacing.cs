using UnityEngine;

namespace Project3D
{
    public class WeaponPlacing : MyMonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponPositionMark[] weaponPosition;

        public override void LoadComponent()
        {
            base.LoadComponent();
            weapon = GetComponent<Weapon>();
            weaponPosition = FindObjectsOfType<WeaponPositionMark>();
        }

        private void Start()
        {
            weapon.EquipEvent += OnEquip;
        }

        private void OnDestroy()
        {
            weapon.EquipEvent -= OnEquip;
        }

        private void OnEquip(WeaponSO weaponSO)
        {
            foreach (WeaponPositionMark weaponPlace in weaponPosition)
            {
                weaponPlace.Unequip();
                weaponPlace.Initialize(weaponSO.Model);
            }

            LoadWeapon(WeaponPosition.Back);
        }

        public void LoadWeapon(WeaponPosition location)
        {
            foreach (WeaponPositionMark holder in weaponPosition)
            {
                if (holder.Location == location)
                {
                    holder.Load();
                }
                else
                {
                    holder.Unload();
                }
            }
        }
    }
}
