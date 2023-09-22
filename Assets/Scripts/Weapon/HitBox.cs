using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project3D
{
    public class HitBox : MyMonoBehaviour
    {
        public string AttackName;

        public void DealDamage(float damage, LayerMask damageableLayer)
        {
            Collider[] damagealbes = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation, damageableLayer);
            List<Health> healths = new();

            foreach (Collider damageable in damagealbes)
            {
                var health = damageable.GetComponentInParent<Health>();
                if (health != null && !healths.Contains(health))
                {
                    healths.Add(health);
                    health.TakeDamage(damage);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, transform.localScale);
        }
    }
}
