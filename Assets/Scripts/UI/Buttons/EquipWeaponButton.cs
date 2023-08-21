using UnityEngine;

namespace Project3D
{
    public class EquipWeaponButton : ButtonBinder
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponSO weaponSO;

        public override void LoadComponent()
        {
            base.LoadComponent();
            weapon = FindObjectOfType<Weapon>();
        }

        protected override void OnClick()
        {
            weapon.Equip(weaponSO);
        }
    }
}
