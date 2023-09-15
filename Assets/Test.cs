using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project3D
{
    public class Test : MonoBehaviour
    {
        public bool bite;

        public Animator animator;



        private void Update()
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                Bite();
            }
        }

        private void Bite()
        {
            animator.SetTrigger("Bite");
        }
    }
}
