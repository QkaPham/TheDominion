using TMPro;
using UnityEngine;

namespace Project3D
{
    public class UpgradeCostTextBinder : MyMonoBehaviour
    {
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Stat stat;

        public override void LoadComponent()
        {
            base.LoadComponent();

            costText = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            stat.OnLevelChange += UpdateCost;
        }

        private void OnDisable()
        {

            stat.OnLevelChange += UpdateCost;
        }

        private void UpdateCost(int level)
        {
            if (level < Stat.MaxLevel)
                costText.text = "Cost : " + stat.UpgradePrice() + "G";
            else
                costText.text = "";
        }
    }
}