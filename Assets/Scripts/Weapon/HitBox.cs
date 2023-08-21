using UnityEngine;

namespace Project3D
{
    public class HitBox : MyMonoBehaviour
    {
        public string AttackName;

        public void DealDamage(float damage, LayerMask damageableLayer)
        {
            Collider[] damagealbes = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation, damageableLayer);
            foreach (Collider damageable in damagealbes)
            {
                if (damageable.TryGetComponent<Health>(out var health))
                {
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

        //[SerializeField] Transform player;
        //[SerializeField] private Vector3 position;
        //[SerializeField] private Vector3 rotation;
        //[SerializeField] private Vector3 size;

        //public void DealDamage(float damage, LayerMask damageableLayer)
        //{
        //    Collider[] damagealbes = Physics.OverlapBox(player.TransformPoint(position), Vector3.Scale(size, player.localScale) / 2, player.rotation * Quaternion.Euler(rotation), damageableLayer);

        //    foreach (Collider damageable in damagealbes)
        //    {
        //        if (damageable.TryGetComponent<Health>(out var health))
        //        {
        //            health.TakeDamage(damage);
        //        }
        //    }
        //}

        //private void OnDrawGizmos()
        //{
        //    Gizmos.matrix = Matrix4x4.TRS(player.TransformPoint(position), player.rotation * Quaternion.Euler(rotation), Vector3.Scale(size, player.localScale));
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireCube(Vector3.zero, transform.localScale);
        //}
    }
}
