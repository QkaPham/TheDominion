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

        private void OnEnable() => button.onClick.AddListener(OnClick);
        private void OnDisable() => button.onClick.RemoveListener(OnClick);

        protected abstract void OnClick();
    }
}
