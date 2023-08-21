using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class UpgradeStatButton : ButtonBinder
    {
        [SerializeField] private Stat stat;
        [SerializeField] private Currency currency;

        public override void LoadComponent()
        {
            base.LoadComponent();
            currency = FindObjectOfType<Currency>();
        }

        protected override void OnClick()
        {
            if (stat.IsMax()) return;
            if (currency.Gold < stat.UpgradePrice()) return;

            currency.Use(stat.UpgradePrice());
            stat.Upgrade();
        }
    }
}
