using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Project3D
{
    public class AnimationEventFireBreath : MyMonoBehaviour
    {
        public TargetDetector TargetDetector;
        public Transform headAim;
        public Rig headRig;
        public float speed;

        public event Action FireBreathStart, FireBreathEnd;

        public void FireBreathStartEvent()
        {
            FireBreathStart?.Invoke();
        }

        public void FireBreathEndEvent()
        {
            FireBreathEnd?.Invoke();
        }

        public void SpecialStartEvent()
        {
            FireBreathStart?.Invoke();
            StartCoroutine(FollowTarget());
        }

        public void SpecialEndEvent()
        {
            FireBreathEnd?.Invoke();
            StopCoroutine(FollowTarget());
        }

        private IEnumerator FollowTarget()
        {
            TargetDetector.Look = true;
            while (true)
            {
                headAim.position = Vector3.MoveTowards(headAim.position, TargetDetector.Target.position, Time.deltaTime * speed);
                yield return null;
            }
        }
    }
}
