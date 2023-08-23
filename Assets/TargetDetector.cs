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
        public bool Look { get; set; }

        private Collider[] targets;
        [field: SerializeField] public Transform Target { get; private set; }
        public Vector3 TargetDirection => Target.position - transform.position;
        public float DistanceToTarget => Vector3.Distance(transform.position, Target.position);

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

            Target = targets[0].transform;
        }

        public void GetTargetInRadius()
        {
            System.Array.Clear(targets, 0, maxDetectNumber);
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, targets, targetLayer) <= 0)
            {
                Target = null;
                return;
            }

            Target = targets[0].transform;
        }

        private void LookAtTarget()
        {
            if (HasTarget() && Look)
            {
                lookAtRig.weight = Mathf.MoveTowards(lookAtRig.weight, 1, lookSmoothTime);
                lookAtPoint.transform.position = Target.position;
            }
            else
            {
                lookAtRig.weight = Mathf.MoveTowards(lookAtRig.weight, 0, lookSmoothTime);
            }
        }
    }
}

