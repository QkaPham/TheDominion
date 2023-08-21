using UnityEngine;

namespace Project3D
{
    public class SoundEffectsPlayer : MyMonoBehaviour
    {
        [field: SerializeField] public static AudioSource AudioSource { get; private set; }

        public override void LoadComponent()
        {
            base.LoadComponent();
            AudioSource = GetComponent<AudioSource>();
            AudioSource.playOnAwake = false;
        }
    }
}
