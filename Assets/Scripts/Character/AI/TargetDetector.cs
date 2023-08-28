using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Project3D
{
    public class TargetDetector : MyMonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer = 1 << 7;
        [SerializeField] private float distance = 2;
        [SerializeField] private float angle = 180;
        [SerializeField] private int maxDetectNumber = 1;
        [SerializeField] private float radius = 3;
        [SerializeField] private Transform lookAtPoint;
        [SerializeField] private Rig lookAtRig;
        [SerializeField] private float lookSmoothTime = 0.1f;
        [SerializeField] private LayerMask groundLayer = 1 << 0 | 1 << 3;

        public bool Look { get; set; } = true;

        private Collider[] targets;
        [field: SerializeField] public Transform Target { get; private set; }
        public Vector3 TargetDirection => Target.position - transform.position;
                                                                                                             
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

        private void Update()
        {
            LookAtTarget();
        }

        public bool HasTarget() => Target != null;

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

        private void LookAtTarget()
        {
            if (HasTarget() && Look)
            {
                if (lookAtRig.weight < 1)
                    lookAtRig.weight = Mathf.MoveTowards(lookAtRig.weight, 1, lookSmoothTime);
                lookAtPoint.transform.position = Target.position;
            }
            else
            {
                if (lookAtRig.weight > 0)
                    lookAtRig.weight = Mathf.MoveTowards(lookAtRig.weight, 0, lookSmoothTime);
            }
        }
    }
}

