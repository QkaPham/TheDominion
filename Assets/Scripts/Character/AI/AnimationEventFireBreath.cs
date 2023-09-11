using System;
using UnityEngine;

namespace Project3D
{
    public class AnimationEventFireBreath : MyMonoBehaviour
    {
        [SerializeField] private AILook aiLook;
        public event Action FireBreathStart, FireBreathEnd;

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
            FireBreathStart?.Invoke();
        }

        public void SpecialEndEvent()
        {
            FireBreathEnd?.Invoke();
            aiLook.Stop();
        }
    }
}
