using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project3D
{
    public class Test : MonoBehaviour
    {
        public ParticleSystem[] particles;
        private float time;

        private void Start()
        {
            StartCoroutine(Move());
        }

        private void Update()
        {


            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                System.Array.ForEach(particles, particle => particle.Play());
            }
        }

        private IEnumerator Move()
        {
            while (true)
            {
                if (time < 2)
                {
                    time += Time.deltaTime;
                    transform.position += Vector3.right * Time.deltaTime;
                }
                if (time < 4 && time >= 2)
                {
                    time += Time.deltaTime;
                    transform.position -= Vector3.right * Time.deltaTime;
                }
                if (time >= 4)
                {
                    time = 0;
                }
                yield return null;
            }
        }
    }
}
