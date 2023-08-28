using TMPro;
using UnityEngine;

namespace Project3D
{
    public class PotionQuantityTextBinder : MyMonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Potion potion;

        public override void LoadComponent()
        {
            base.LoadComponent();

            text = GetComponent<TMP_Text>();
            potion = FindObjectOfType<Potion>();
        }

        private void UpdateText()
        {
            text.text = "";
        }
    }
}
