using TMPro;
using UnityEngine;

namespace Project3D
{
    public class InteractTextBinder : MyMonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public override void LoadComponent()
        {
            base.LoadComponent();
            text = GetComponent<TMP_Text>();
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}
