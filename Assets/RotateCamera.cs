using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float rotateAngle = 45f;
        [SerializeField] private float rotateSpeed = 45f;

        private bool isRotating;

        private void OnEnable()
        {
            input.RotateCamera += Rotate;
        }
        private void OnDisable()
        {
            input.RotateCamera -= Rotate;
        }

        private void Rotate(float direction)
        {
            StartCoroutine(RotateCoroutine(direction));
        }

        private IEnumerator RotateCoroutine(float direction)
        {
            if (isRotating) yield break;

            isRotating = true;
            var targetRotation = Quaternion.AngleAxis(-direction * rotateSpeed, Vector3.up) * cameraTransform.rotation;
            while (cameraTransform.rotation != targetRotation)
            {
                cameraTransform.rotation = Quaternion.RotateTowards(cameraTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                yield return null;
            }
            isRotating = false;
        }
    }
}
