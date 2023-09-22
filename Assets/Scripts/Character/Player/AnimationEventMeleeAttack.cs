using UnityEngine;

namespace Project3D
{
    public class AnimationEventMeleeAttack : MyMonoBehaviour
    {
        public event System.Action AttackFinish;
        public event System.Action<string> AttackDealDamage;

        public void AttackDealDamageEvent(string attackName)
        {
            AttackDealDamage?.Invoke(attackName);
        }

        public void AttackFinishEvent()
        {
            AttackFinish?.Invoke();
        }
    }
}
