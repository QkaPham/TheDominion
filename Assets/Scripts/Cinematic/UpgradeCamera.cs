using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class UpgradeCamera : MonoBehaviour
    {
        [SerializeField] private Transform player;

        private Vector3 rotation;
        private void OnEnable()
        {
            CheckPoint.InteractEvent += FacingPlayer;
        }

        private void OnDisable()
        {
            CheckPoint.InteractEvent -= FacingPlayer;
        }

        private void Start()
        {
            rotation = transform.rotation.eulerAngles;
        }

        public void FacingPlayer()
        {
            rotation.y = player.rotation.eulerAngles.y + 180;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
