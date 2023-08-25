using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        private void Update()
        {
            if (input.Pause)
            {
                //show pause view
            }

            if (input.Cancel)
            {
                //show last view
            }
        }

        private BaseView GetView(Type viewType) => views.FirstOrDefault(view => view.GetType() == viewType);
        
        public void ClearHistory() => history.Clear();

        public void Show(Type viewType, Action onComplete, bool remember)
        {
            var view = GetView(viewType);

            if (view != null) return;

            view.Show(onComplete);

            if (remember) history.Push(currentView);
            currentView = view;
        }

        private void ShowAndHide(Type viewType, Action onComplete, bool remember)
        {
            var view = GetView(viewType);

            if (view != null) return;

            currentView.Hide(() =>
            {
                view.Show(onComplete);
            });

            if (remember) history.Push(currentView);
            currentView = view;
        }

        public void ShowLast(Action onComplete)
        {
            if (history.Peek() == null) return;

            var lastView = history.Pop();

            if (!lastView.isShowing)
            {
                ShowAndHide(lastView.GetType(), onComplete, false);
            }
            else
            {
                currentView.Hide(null);
            }

        }
    }

    public class BaseView : MyMonoBehaviour
    {
        public bool isShowing { get; set; }

        public virtual void Show(Action onComplete)
        {
            isShowing = true;
        }

        public virtual void Hide(Action onComplete)
        {
            isShowing = false;
        }
    }
}
