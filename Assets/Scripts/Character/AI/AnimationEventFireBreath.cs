using System;
using UnityEngine;

namespace Project3D
{
    public class AnimationEventFireBreath : MyMonoBehaviour
    {
        [SerializeField] private AILook aiLook;
        public event Action FireBreathStart, FireBreathEnd, SpecialStart, SpecialEnd;

        public UpperBodyRig upperBodyRig;

        public void FireBreathStartEvent()
        {
            FireBreathStart?.Invoke();
        }

        public void FireBreathEndEvent()
        {
            FireBreathEnd?.Invoke();
        }

        public void SpecialLookEvent()
        {
            aiLook.Follow();
        }

        public void SpecialStartEvent()
        {
            SpecialStart?.Invoke();
        }

        public void SpecialEndEvent()
        {
            SpecialEnd?.Invoke();
            aiLook.Stop();
        }


        public void StumpEnd()
        {
        }
    }
}
