using System;
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
            Array.ForEach(views, view => view.gameObject.SetActive(true));
            currentView = GetView(typeof(GameView));
        }

        private void Update()
        {
            if (input.Pause)
            {
                Show(typeof(PauseView));
            }

            if (input.Cancel)
            {
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
            var lastView = history.Pop();
            currentView.Hide(() => lastView.Show(onComplete));
            currentView = lastView;
        }
    }
}
