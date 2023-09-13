using UnityEngine;

namespace Project3D
{
    public class TargetDetector : MyMonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer = 1 << 7;
        [SerializeField] private float distance = 2;
        [SerializeField] private float angle = 180;
        [SerializeField] private int maxDetectNumber = 1;
        [SerializeField] private float radius = 3;
        [SerializeField] private LayerMask groundLayer = 1 << 0 | 1 << 3;

        private Collider[] targets;

        private Transform target;
        public Transform Target
        {
            get => target;
            set
            {
                target = value;
                targetHealth = target == null ? null : target.root.GetComponent<PlayerHealth>();
            }
        }

        [field: SerializeField] public PlayerHealth targetHealth { get; private set; }
        public Vector3 TargetDirection
        {
            get
            {
                var result = Target.position - transform.position;
                return result == Vector3.zero ? transform.forward : result;
            }
        }

        public float DistanceToTarget
        {
            get
            {
                if (!HasTarget()) return float.MaxValue;
                return Vector3.Distance(transform.position, Target.position);
            }
        }

        public Vector3 Destination
        {
            get
            {
                if (!HasTarget()) return transform.position;
                Physics.Raycast(Target.position, Vector3.down, out var hitInfo, 256f, groundLayer);
                return hitInfo.point;
            }
        }

        public float DistanceToDestination => Vector3.Distance(transform.position, Destination);


        private void Awake()
        {
            maxDetectNumber = Mathf.Max(1, maxDetectNumber);
            targets = new Collider[maxDetectNumber];
        }

        public bool HasTarget() => Target != null && !targetHealth.IsDead;

        public void GiveUp() => Target = null;

        public void GetTargetForward()
        {
            System.Array.Clear(targets, 0, maxDetectNumber);
            if (Physics.OverlapSphereNonAlloc(transform.position, distance, targets, targetLayer) <= 0)
            {
                Target = null;
                return;
            }

            var direction = targets[0].transform.position - transform.position;
            if (Vector3.Angle(transform.forward, direction) > angle / 2f)
            {
                Target = null;
                return;
            }

            Target = targets[0].transform.root.GetComponent<PlayerController>().TargetPoint;
        }

        public void GetTargetInRadius()
        {
            System.Array.Clear(targets, 0, maxDetectNumber);
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, targets, targetLayer) <= 0)
            {
                Target = null;
                return;
            }

            Target = targets[0].transform.root.GetComponent<PlayerController>().TargetPoint;
        }
    }
}

