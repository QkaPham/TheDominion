using UnityEngine;

namespace Project3D
{
    public class WorldWeapon : MyMonoBehaviour, IInteractable
    {
        [SerializeField] private WeaponSO weaponSO;

        public bool CanInteract { get; set; } = true;

        public string Content => weaponSO.WeaponName;

        public void Interact(PlayerController player)
        {
            player.GetComponent<WeaponStorage>().Add(weaponSO);
            CanInteract = false;
            Destroy(gameObject);
        }
    }
}
