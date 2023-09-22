using System.Linq;
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

        private void OnEnable()
        {
            weapon.EquipEvent += OnEquip;
        }

        private void OnDisable()
        {
            weapon.EquipEvent -= OnEquip;
        }

        private void OnEquip(WeaponSO weaponSO)
        {
            foreach (WeaponPositionMark weaponPlace in weaponPosition)
            {
                weaponPlace.Unequip();
                var model = weaponSO.WeaponModels.FirstOrDefault(model => model.Position == weaponPlace.Position);
                if (model != null)
                {
                    weaponPlace.Initialize(model.GaneObject);
                }
            }

            LoadWeaponToBack();
        }

        public void LoadWeaponToHand()
        {
            foreach (WeaponPositionMark holder in weaponPosition)
            {
                if (holder.Position == WeaponPosition.Back)
                {
                    holder.Unload();
                }
                else
                {
                    holder.Load();
                }
            }
        }

        public void LoadWeaponToBack()
        {
            foreach (WeaponPositionMark holder in weaponPosition)
            {
                if (holder.Position == WeaponPosition.Back)
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
