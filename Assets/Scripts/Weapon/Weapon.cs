using UnityEngine;

namespace Project3D
{
    public class Weapon : MyMonoBehaviour
    {
        [SerializeField] private Animator animator;
        [field: SerializeField] public WeaponSO EquipedWeapon { get; private set; }
        public event System.Action<WeaponSO> EquipEvent;

        public override void LoadComponent()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Start() => Equip(EquipedWeapon);

        public void Equip(WeaponSO weapon)
        {
            EquipedWeapon = weapon;

            animator.runtimeAnimatorController = EquipedWeapon.AnimatorOverride;
            EquipEvent?.Invoke(weapon);
        }
    }
}
