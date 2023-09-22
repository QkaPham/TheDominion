using UnityEngine;

namespace Project3D
{
    public class AnimationEventPlaySfx : MyMonoBehaviour
    {
        [SerializeField] private SoundEffectsPlayer effectPlayer;

        public void PlaySound(string soundName)
        {
            effectPlayer.Play(soundName);
        }
    }
}
