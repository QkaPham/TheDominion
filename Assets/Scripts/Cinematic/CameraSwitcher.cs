using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project3D
{
    public enum CameraID
    {
        TopDown,
        Upgrade
    }

    public class CameraSwitcher : MyMonoBehaviour
    {
        [field: SerializeField] public CinemachineVirtualCamera TopdownCamera { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera UpgradeCamera { get; private set; }
        [SerializeField] private float transitionTime = 2f;

        private CinemachineVirtualCamera currentCamera;
        private event Action OnTransitionComplete;

        private void Start()
        {
            currentCamera = TopdownCamera;
            TopdownCamera.Priority = 100;
            transitionTime = GetComponentInChildren<CinemachineBrain>().m_DefaultBlend.m_Time;
        }

        private void Update()
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                SwitchCamera(TopdownCamera);
            }
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                SwitchCamera(UpgradeCamera);
            }
        }

        public void SwitchCamera(CinemachineVirtualCamera toCamera, Action onComplete = null)
        {
            currentCamera.Priority = 0;
            currentCamera = toCamera;
            currentCamera.Priority = 100;

            OnTransitionComplete = onComplete;
            StartCoroutine(TransitionCompleteTimer(transitionTime));
        }

        private IEnumerator TransitionCompleteTimer(float transitionTime)
        {
            yield return new WaitForSeconds(transitionTime);
            OnTransitionComplete?.Invoke();
        }
    }
}
