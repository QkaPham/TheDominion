using UnityEngine;

namespace Project3D
{
    public class ItemCollector : MyMonoBehaviour
    {
        [SerializeField] private Currency currency;

        public override void LoadComponent()
        {
            base.LoadComponent();
            currency = GetComponent<Currency>();
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.TryGetComponent<Gold>(out var gold))
        //    {
        //        inventory.Add(gold.value);
        //    }
        //}
    }
}
