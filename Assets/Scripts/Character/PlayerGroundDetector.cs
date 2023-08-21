using UnityEngine;

namespace Project3D
{
    public class PlayerGroundDetector : MyMonoBehaviour
    {
        [SerializeField] float detectionRadius = 0.1f;
        [SerializeField] LayerMask groundLayer;
        Collider[] colliders = new Collider[1];

        public bool IsGrounded => Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, groundLayer) != 0;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}