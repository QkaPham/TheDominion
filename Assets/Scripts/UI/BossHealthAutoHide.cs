using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class BossHealthAutoHide : MonoBehaviour
    {
        [SerializeField] private TargetDetector targetDetector;
        [SerializeField] private BossHealthUI healthUI;

        private void Update()
        {
            if (targetDetector.HasTarget())
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Show()
        {
            healthUI.gameObject.SetActive(true);
        }

        public void Hide()
        {
            healthUI.gameObject.SetActive(false);
        }
    }
}
