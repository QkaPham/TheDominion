using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project3D
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private ParticleSystem flameVFX;

        private void Update()
        {
            if (Keyboard.current.uKey.wasPressedThisFrame)
            {
                flameVFX.Play();
            }
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                flameVFX.Stop();
            }
        }
    }
}
