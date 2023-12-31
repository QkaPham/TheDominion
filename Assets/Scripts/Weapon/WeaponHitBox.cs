using System.Linq;
using UnityEngine;

namespace Project3D
{
    public class WeaponHitBox : MyMonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private AnimationEventMeleeAttack animationEvent;

        [SerializeField] private Stat strength;
        [SerializeField] private LayerMask damageableLayer;

        private Transform hitBoxParent;
        private HitBox[] hitBoxes;

        public override void LoadComponent()
        {
            base.LoadComponent();
            weapon = GetComponent<Weapon>();
            animationEvent = GetComponentInChildren<AnimationEventMeleeAttack>();
        }

        private void OnEnable()
        {
            animationEvent.AttackDealDamage += DealDamage;
            weapon.EquipEvent += CreateHitBoxs;
        }

        private void OnDisable()
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
            hitBoxes = hitBoxParent.GetComponentsInChildren<HitBox>();
        }

        private void DealDamage(string attackName)
        {
            foreach (HitBox hitBox in hitBoxes)
            {
                if (hitBox.AttackName == attackName)
                {
                    hitBox.DealDamage(strength.Value, damageableLayer);
                    return;
                }
            }
        }
    }
}
