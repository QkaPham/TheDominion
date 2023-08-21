using TMPro;
using UnityEngine;

namespace Project3D
{
    public class GoldUI : MyMonoBehaviour
    {
        [SerializeField] private TMP_Text goldUiText;
        [SerializeField] private Currency currency;

        public override void LoadComponent()
        {
            base.LoadComponent();
            goldUiText = GetComponent<TMP_Text>();
            currency = FindObjectOfType<Currency>();
        }

        private void OnEnable() => currency.GoldChange += UpdateGold;

        private void OnDisable() => currency.GoldChange -= UpdateGold;

        public void UpdateGold(int value)
        {
            goldUiText.text = value.ToString();
        }
    }
}
