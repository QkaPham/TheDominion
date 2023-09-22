using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project3D
{
    public class VisualEffectsPlayer : MonoBehaviour
    {
        public Effect[] effects;

        public void Play(string effectName)
        {
            effects.FirstOrDefault(effect => effect.name == effectName)?.Play();
            //var eff = effects.FirstOrDefault(effect => effect.name == effectName);
            //if (eff != null)
            //{
            //    eff.Play();
            //}
        }

        public void Stop(string effectName)
        {
            effects.FirstOrDefault(effect => effect.name == effectName)?.Stop();
        }

        [Serializable]
        public class Effect
        {
            public string name;
            public List<ParticleSystem> effects;

            public void Play() => effects.ForEach(partical => partical.Play());
            public void Stop() => effects.ForEach(partical => partical.Stop());
        }
    }
}
