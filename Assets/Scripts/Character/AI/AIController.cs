using System.Collections;
using UnityEngine;

namespace Project3D
{
    public class AIController : MyMonoBehaviour
    {
        public bool CanAttack { get; private set; } = true;

        public void StopAttackFor(float seconds) => StartCoroutine(StopAttackCoroutine(seconds));

        private IEnumerator StopAttackCoroutine(float seconds)
        {
            CanAttack = false;
            yield return new WaitForSeconds(seconds);
            CanAttack = true;
        }

        public class Skill
        {
            public string name;
            public float cooldown;
            
            public void Use()
            {
                //switch state
            }
        }
    }
}
