using UnityEngine;

namespace Project3D
{
    public class CanvasRotate : MyMonoBehaviour
    {
        [SerializeField] private Camera mainCam;

        public override void LoadComponent()
        {
            base.LoadComponent();
            mainCam = Camera.main;
        }

        private void Update()
        {
            transform.rotation = mainCam.transform.rotation;
        }
    }
}
