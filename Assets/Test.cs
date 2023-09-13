using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class Test : MonoBehaviour
    {
        public Vector2 pos;

        private void Reset()
        {
            pos = Camera.main.WorldToScreenPoint(transform.position);
        }
    }
}
