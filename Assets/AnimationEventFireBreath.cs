using System;

namespace Project3D
{
    public class AnimationEventFireBreath : MyMonoBehaviour
    {
        public event Action FireBreathStart, FireBreathEnd;

        public void FireBreathStartEvent()
        {
            FireBreathStart?.Invoke();
        }

        public void FireBreathEndEvent()
        {
            FireBreathEnd?.Invoke();
        }
    }
}
