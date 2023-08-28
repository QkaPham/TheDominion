using UnityEngine;

namespace Project3D
{
    public class CanvasRotate : MyMonoBehaviour
    {
        private Camera mainCam;

        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void Update()
        {
            transform.rotation = mainCam.transform.rotation;
        }
    }
}
