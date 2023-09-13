using TMPro;
using UnityEngine;

namespace Project3D
{
    public class EquipWeaponButton : ButtonBinder
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] public WeaponSO weaponSO;
        [SerializeField] private WeaponStorage weaponStorage;
        [SerializeField] private TMP_Text text;

        public override void LoadComponent()
        {
            base.LoadComponent();
            weapon = FindObjectOfType<Weapon>();
            weaponStorage = FindObjectOfType<WeaponStorage>();
            text = GetComponentInChildren<TMP_Text>();
            weaponSO = weaponStorage.ownedWeapons[transform.GetSiblingIndex()];
            text.text = weaponSO.WeaponName;
        }

        protected override void OnClick()
        {
            weapon.Equip(weaponSO);
        }
    }
}
