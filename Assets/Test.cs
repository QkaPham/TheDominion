using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project3D
{
    public class Test : MonoBehaviour
    {
        private void Update()
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                transform.parent = null;
            }
        }
    }
}
