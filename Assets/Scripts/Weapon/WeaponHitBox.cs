using System.Linq;
using UnityEngine;

namespace Project3D
{
    public class WeaponHitBox : MyMonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private PlayerAnimationEvent animationEvent;

        [SerializeField] private Stat strength;
        [SerializeField] private LayerMask damageableLayer;

        private Transform hitBoxParent;
        private HitBox[] hitBoxs;

        public override void LoadComponent()
        {
            base.LoadComponent();
            weapon = GetComponent<Weapon>();
            animationEvent = GetComponentInChildren<PlayerAnimationEvent>();
        }

        private void Start()
        {
            animationEvent.AttackDealDamage += DealDamage;
            weapon.EquipEvent += CreateHitBoxs;
        }

        private void OnDestroy()
        {
            animationEvent.AttackDealDamage -= DealDamage;
            weapon.EquipEvent -= CreateHitBoxs;
        }

        private void CreateHitBoxs(WeaponSO weaponSO)
        {
            if (hitBoxParent != null) Destroy(hitBoxParent.gameObject);

            hitBoxParent = Instantiate(weaponSO.HitBoxsPrefab, transform);
            hitBoxParent.localPosition = Vector3.zero;
            hitBoxParent.localRotation = Quaternion.identity;
            hitBoxs = hitBoxParent.GetComponentsInChildren<HitBox>();
        }

        private void DealDamage(string attackName)
        {
            var hitbox = GetHitBox(attackName);
            if (hitbox != null) hitbox.DealDamage(strength.Value, damageableLayer);
        }

        private HitBox GetHitBox(string attackName)
        {
            return hitBoxs.FirstOrDefault(hitBox => hitBox.AttackName == attackName);
        }
    }
}
