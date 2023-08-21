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
        private Collider[] targets;

        [field: SerializeField] public Transform Target { get; private set; }
        public Vector3 TargetDirection => Target.position - transform.position;
        public float DistanceToTarget => Vector3.Distance(transform.position, Target.position);

        private void Awake()
        {
            maxDetectNumber = Mathf.Max(1, maxDetectNumber);
            targets = new Collider[maxDetectNumber];
        }

        public bool HasTargetForward()
        {
            if (Target != null) return true;
            Target = GetTargetForward();
            return Target != null;
        }

        public bool HasTargetInRadius()
        {
            if (Target != null) return true;
            Target = GetTargetInRadius();
            return Target != null;
        }

        public void GiveUp() => Target = null;

        private Transform GetTargetForward()
        {
            System.Array.Clear(targets, 0, maxDetectNumber);
            if (Physics.OverlapSphereNonAlloc(transform.position, distance, targets, targetLayer) <= 0) return null;

            var direction = targets[0].transform.position - transform.position;
            if (Vector3.Angle(transform.forward, direction) > angle / 2f) return null;

            return targets[0].transform;
        }

        private Transform GetTargetInRadius()
        {
            System.Array.Clear(targets, 0, maxDetectNumber);
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, targets, targetLayer) <= 0) return null;

            return targets[0].transform;
        }
    }
}
