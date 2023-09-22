using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Project3D
{
    public class AILook : MyMonoBehaviour
    {
        [SerializeField] private TargetDetector targetDetector;
        [SerializeField] private Rig headRig;
        [SerializeField] private Transform lookAtSource;
        [SerializeField] private float lookSmoothTime = 1f;
        [SerializeField] private float followSpeed = 1f;

        public void LockOn()
        {
            StopAllCoroutines();
            StartCoroutine(WeightUpdate(1));
            StartCoroutine(LockOnTarget());
        }

        public void Follow()
        {
            StopAllCoroutines();
            StartCoroutine(WeightUpdate(1));
            StartCoroutine(FollowTarget());
        }

        public void Stop()
        {
            StopAllCoroutines();
            StartCoroutine(WeightUpdate(0));
            StartCoroutine(LockOnTarget());
        }

        public IEnumerator WeightUpdate(float newWeight)
        {
            while (headRig.weight != newWeight)
            {
                headRig.weight = Mathf.MoveTowards(headRig.weight, newWeight, Time.deltaTime / lookSmoothTime);
                yield return null;
            }
        }

        private Vector3 nextPosition;

        private IEnumerator FollowTarget()
        {
            while (true)
            {
                if (targetDetector.HasTarget())
                {
                   var nextPosition = Vector3.MoveTowards(lookAtSource.position, targetDetector.Target.position, Time.deltaTime * followSpeed);
                    lookAtSource.position = nextPosition;
                }
                yield return null;
            }
        }

        private IEnumerator LockOnTarget()
        {
            while (true)
            {
                if (targetDetector.HasTarget())
                {
                    lookAtSource.position = targetDetector.Target.position;
                }
                yield return null;
            }
        }
    }
}
