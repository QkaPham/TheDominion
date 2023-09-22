using UnityEngine;

namespace Project3D
{
    public class AnimationEventPlayVfx : MonoBehaviour
    {
        [SerializeField] private VisualEffectsPlayer effectsPlayer;

        public void PlayEffect(string effectName)
        {
            effectsPlayer.Play(effectName);
        }
    }
}
