using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Project3D
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float minFOV = 40f;
        [SerializeField] private float maxFOV = 60f;
        [SerializeField] private float diffFOV = 5f;
        [SerializeField] private AnimationCurve rotationCurve;
        [SerializeField] private Transform cameraTransform;

        private bool isZooming;

        private void OnEnable()
        {
            input.ZoomCamera += Zoom;
        }

        private void OnDisable()
        {
            input.ZoomCamera -= Zoom;
        }

        //private void Update()
        //{
        //    if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        //    {
        //        Zoom(1);
        //    }
        //    if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        //    {
        //        Zoom(-1);
        //    }
        //}

        private void Zoom(float direction)
        {
            ZoomTo(Mathf.Clamp(virtualCamera.m_Lens.FieldOfView - direction * diffFOV, minFOV, maxFOV));
        }

        private void ZoomTo(float toFOV)
        {
            StopAllCoroutines();
            StartCoroutine(ZoomToCoroutine(toFOV));
        }

        private IEnumerator ZoomToCoroutine(float toFOV)
        {
            isZooming = true;
            while (Mathf.Abs(virtualCamera.m_Lens.FieldOfView - toFOV) > 0.01f)
            {
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, toFOV, zoomSpeed * Time.deltaTime);

                Vector3 eulerRotation = cameraTransform.rotation.eulerAngles;
                eulerRotation.x = rotationCurve.Evaluate(virtualCamera.m_Lens.FieldOfView);
                cameraTransform.rotation = Quaternion.Euler(eulerRotation);

                yield return null;
            }
            virtualCamera.m_Lens.FieldOfView = toFOV;
            isZooming = false;
        }
    }
}
