using System;
using System.Collections;
using UnityEngine;

namespace Project3D
{
    public class BaseView : MyMonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected PlayerInput input;
        [SerializeField] protected ViewAnimate[] viewAnimates;
        public float duration = 1f;
        [field: SerializeField] public bool IsShowing { get; set; } = true;

        protected virtual void Start()
        {
            StartCoroutine(PlayAnimation(false, 0, null));
        }

        public virtual void Show(Action onComplete)
        {
            PreShow();
            StartCoroutine(PlayAnimation(true, duration, () =>
            {
                PostShow();
                onComplete?.Invoke();
            }));
        }

        public virtual void Hide(Action onComplete)
        {
            PreHide();
            StartCoroutine(PlayAnimation(false, duration, () =>
            {
                PostHide();
                onComplete?.Invoke();
            }));
        }

        protected virtual void PreShow()
        {
            input.EnablePlayerInput(false);
            input.EnableUIInput(false);
        }

        protected virtual void PostShow()
        {
            input.EnableUIInput(true);
        }

        protected virtual void PreHide()
        {
            input.EnableUIInput(false);
        }

        protected virtual void PostHide()
        {
            input.EnableUIInput(true);
        }

        protected IEnumerator PlayAnimation(bool show, float duration, Action onComplete)
        {
            if (IsShowing != show)
            {
                IsShowing = show;
                canvasGroup.interactable = show;
                canvasGroup.blocksRaycasts = show;
                Array.ForEach(viewAnimates, show ? view => view.Show(duration) : view => view.Hide(duration));
                yield return new WaitForSeconds(duration);
            }
            onComplete?.Invoke();
        }
    }
}
