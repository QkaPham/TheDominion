using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project3D
{
    public class AutoHide : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private float time = 2f;
        [SerializeField] private Image image;

        private void OnEnable()
        {
            health.HealthChanged += Show;
        }

        private void Start()
        {
            image.enabled = false; 
        }

        private void Show(float diff)
        {
            image.enabled = true;
            StartCoroutine(Hide());
        }

        private IEnumerator Hide()
        {
            yield return new WaitForSeconds(time);
            image.enabled = false;
        }
    }
}
