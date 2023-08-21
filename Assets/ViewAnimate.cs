using System;
using System.Collections;
using UnityEngine;

namespace Project3D
{
    [Flags]
    public enum ViewAnimationType
    {
        None = 0,
        Slide = 1 << 0,
        Zoom = 1 << 1,
        Fade = 1 << 2,
    }

    [Flags]
    public enum ViewAnimationDirection
    {
        None = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class ViewAnimate : MyMonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;

        [SerializeField] private ViewAnimationType type;
        [SerializeField] private ViewAnimationDirection direction;

        [SerializeField, Range(0f, 1f)] private float slideDistance = 1;

        private Vector3 showPosition;
        private Vector3 hidePosition;

        public event Action ShowCompleteEvent, HideCompleteEvent;
        public bool IsShown { get; private set; }

        public override void LoadComponent()
        {
            base.LoadComponent();
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Awake()
        {
            showPosition = rectTransform.localPosition;
            hidePosition = RecalculateDeactivePosition();
        }

        private void OnValidate()
        {
            showPosition = rectTransform.localPosition;
            hidePosition = RecalculateDeactivePosition();
        }

        public void Show(float duration)
        {
            StartCoroutine(ShowCoroutine(duration));
        }

        public void Hide(float duration)
        {
            StartCoroutine(HideCoroutine(duration));
        }

        private IEnumerator ShowCoroutine(float duration)
        {
            if (duration <= 0f)
            {
                rectTransform.localPosition = showPosition;
                transform.localScale = Vector3.one;
                canvasGroup.alpha = 1;
                yield break;
            }

            float startTime = Time.time;
            float progress = 0f;
            while (progress < 1)
            {
                progress = (Time.time - startTime) / duration;

                if (type.HasFlag(ViewAnimationType.Slide))
                {
                    rectTransform.localPosition = Vector3.Lerp(hidePosition, showPosition, progress);
                }

                if (type.HasFlag(ViewAnimationType.Zoom))
                {
                    transform.localScale = Mathf.Lerp(0, 1, progress) * Vector3.one;
                }

                if (type.HasFlag(ViewAnimationType.Fade))
                {
                    canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
                }

                yield return null;
            }

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            ShowCompleteEvent?.Invoke();
            IsShown = true;
        }

        private IEnumerator HideCoroutine(float duration)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            if (duration <= 0f)
            {
                rectTransform.localPosition = hidePosition;
                transform.localScale = Vector3.zero;
                canvasGroup.alpha = 0;
                yield break;
            }

            float startTime = Time.time;
            float progress = 0f;
            while (progress < 1)
            {
                progress = (Time.time - startTime) / duration;

                if (type.HasFlag(ViewAnimationType.Slide))
                {
                    rectTransform.localPosition = Vector3.Lerp(showPosition, hidePosition, progress);
                }

                if (type.HasFlag(ViewAnimationType.Zoom))
                {
                    transform.localScale = Mathf.Lerp(1, 0, progress) * Vector3.one;
                }

                if (type.HasFlag(ViewAnimationType.Fade))
                {
                    canvasGroup.alpha = Mathf.Lerp(1, 0, progress);
                }

                yield return null;
            }

            HideCompleteEvent?.Invoke();
            IsShown = false;
        }

        private Vector3 RecalculateDeactivePosition()
        {
            var deactivePosition = showPosition;

            if (direction.HasFlag(ViewAnimationDirection.Up))
            {
                deactivePosition += Screen.height * slideDistance * Vector3.down;
            }

            if (direction.HasFlag(ViewAnimationDirection.Down))
            {
                deactivePosition += Screen.height * slideDistance * Vector3.up;
            }

            if (direction.HasFlag(ViewAnimationDirection.Left))
            {
                deactivePosition += Screen.width * slideDistance * Vector3.right;
            }

            if (direction.HasFlag(ViewAnimationDirection.Right))
            {
                deactivePosition += Screen.width * slideDistance * Vector3.left;
            }

            return deactivePosition;
        }
    }
}