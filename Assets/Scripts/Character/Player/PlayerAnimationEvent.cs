using UnityEngine;

namespace Project3D
{
    public class PlayerAnimationEvent : MyMonoBehaviour
    {
        public event System.Action AttackFinish;
        public event System.Action<string> AttackDealDamage;

        private string attack;
        public void AttackDealDamageEvent(string attackName)
        {
            AttackDealDamage?.Invoke(attackName);
            attack = attackName;
        }

        public void AttackFinishEvent()
        {
            Debug.Log(attack + " Finish");
            AttackFinish?.Invoke();
        }
    }
}
