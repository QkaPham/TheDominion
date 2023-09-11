using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project3D
{
    public class UIManager : MyMonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private BaseView[] views;
        [SerializeField] private PlayerInput input;

        private BaseView currentView;
        private Stack<BaseView> history = new Stack<BaseView>();

        public override void LoadComponent()
        {
            base.LoadComponent();
            canvas = GetComponentInChildren<Canvas>();
            views = GetComponentsInChildren<BaseView>();
            input = FindObjectOfType<PlayerInput>();
        }

        private void OnEnable()
        {
            CheckPoint.InteractEvent += ShowUpgradeView;
        }

        private void OnDisable()
        {
            CheckPoint.InteractEvent -= ShowUpgradeView;
        }

        private void Start()
        {
            currentView = GetView(typeof(GameView));
            //currentView.Show(null);
        }

        private void Update()
        {
            if (input.Pause)
            {
                Debug.Log("1");

                Show(typeof(PauseView));
            }

            if (input.Cancel)
            {
                Debug.Log("11");
                ShowLast();
            }
        }

        private void ShowUpgradeView() => Show(typeof(UpgradeView));

        public BaseView GetView(Type viewType) => views.FirstOrDefault(view => view.GetType() == viewType);

        public void ClearHistory() => history.Clear();

        public void Show(Type viewType, Action onComplete = null, bool remember = true)
        {
            var view = GetView(viewType);

            if (view == null) return;

            Debug.Log("2");
            view.Show(onComplete);

            if (remember) history.Push(currentView);
            currentView = view;
        }

        public void HideAndShow(Type viewType, Action onComplete = null, bool remember = true)
        {
            var view = GetView(viewType);
            if (view == null) return;

            currentView.Hide(() => view.Show(onComplete));

            if (remember) history.Push(currentView);
            currentView = view;
        }

        public void ShowLast(Action onComplete = null)
        {
            if (history.Count == 0) return;
            Debug.Log("12");
            var lastView = history.Pop();
            currentView.Hide(() => lastView.Show(onComplete));
            currentView = lastView;
        }
    }

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
