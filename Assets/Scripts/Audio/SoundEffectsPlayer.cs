using System.Linq;
using UnityEngine;

namespace Project3D
{
    public class SoundEffectsPlayer : MyMonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] sounds;

        public override void LoadComponent()
        {
            base.LoadComponent();
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        public void Play(string soundName)
        {
            var clip = sounds.FirstOrDefault(audioClip => audioClip.name == soundName);
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
