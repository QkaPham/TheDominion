using UnityEngine;

namespace Project3D
{
    public class TargetSensor : MyMonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer = 1 << 7;
        [SerializeField] private float distance = 2;
        [SerializeField] private float angle = 180;
        [SerializeField] private int maxDetectNumber = 10;
        private Collider[] targets;

        private void Awake()
        {
            maxDetectNumber = Mathf.Max(1, maxDetectNumber);
            targets = new Collider[maxDetectNumber];
        }

        public Transform GetTargetWithinRadius(float radius = 10)
        {
            System.Array.Clear(targets, 0, maxDetectNumber);
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, targets, targetLayer) <= 0) return null;
            return targets[0].transform;
        }

        public Transform GetTarget()
        {
            System.Array.Clear(targets, 0, maxDetectNumber);
            if (Physics.OverlapSphereNonAlloc(transform.position, distance, targets, targetLayer) <= 0) return null;

            var direction = targets[0].transform.position - transform.position;
            if (Vector3.Angle(transform.forward, direction) > angle / 2f) return null;

            return targets[0].transform;
        }

        public Transform GetClosetTarget()
        {
            System.Array.Clear(targets, 0, maxDetectNumber);
            int amount = Physics.OverlapSphereNonAlloc(transform.position, distance, targets, targetLayer);
            if (amount <= 0) return null;

            Transform closetTarget = null;
            float minDistance = float.MaxValue;

            for (int i = 0; i < amount; i++)
            {
                var direction = targets[i].transform.position - transform.position;

                if (Vector3.Angle(transform.forward, direction) <= angle / 2f)
                {
                    if (direction.magnitude < minDistance)
                    {
                        closetTarget = targets[i].transform;
                    }
                }
            }

            return closetTarget;
        }

        public override void LoadComponent() { }
    }
}