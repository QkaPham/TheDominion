using UnityEngine;
using System.Linq;

namespace Project3D
{
    public class AIHitboxes : MyMonoBehaviour
    {
        [SerializeField] private HitBox[] hitBoxes;
        [SerializeField] private AnimationEventMeleeAttack animationEvent;
        [SerializeField] private float damage = 3f;
        [SerializeField] private LayerMask damageableLayer = 1 << 6;

        public override void LoadComponent()
        {
            base.LoadComponent();

            hitBoxes = GetComponentsInChildren<HitBox>();
            animationEvent = GetComponentInChildren<AnimationEventMeleeAttack>();
        }

        private void Start()
        {
            animationEvent.AttackDealDamage += DealDamage;
        }

        private void DealDamage(string attackName)
        {
            foreach (HitBox hitBox in hitBoxes)
            {
                if (hitBox.AttackName == attackName)
                {
                    hitBox.DealDamage(damage, damageableLayer);
                    return;
                }
            }
        }
    }
}
