using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class WeaponStoragerUI : MonoBehaviour
    {
        [SerializeField] private WeaponStorage weaponStorage;

        private void OnEnable()
        {
            weaponStorage.NewWeaponAdd += OnNewWeaponAdd;
        }

        private void OnNewWeaponAdd(WeaponSO weaponSO)
        {
            var button = GetComponentsInChildren<EquipWeaponButton>().FirstOrDefault(button => button.weaponSO == weaponSO);
            if (button != null)
            {
                button.GetComponent<Button>().interactable = true; 
            }
        }
    }
}
