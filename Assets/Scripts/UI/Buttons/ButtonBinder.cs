using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public abstract class ButtonBinder : MyMonoBehaviour
    {
        [SerializeField] private Button button;

        public override void LoadComponent()
        {
            base.LoadComponent();
            button = GetComponent<Button>();
        }

        private void Start() => button.onClick.AddListener(OnClick);

        private void OnDestroy() => button.onClick.RemoveListener(OnClick);
        protected abstract void OnClick();
    }
}
