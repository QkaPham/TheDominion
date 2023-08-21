using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class Gold : MyMonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
        [SerializeField] private float collectDistance;
        [SerializeField] private float moveToWardDistance;

        private Vector3 velocity;
        private Transform player;
        private int value;

        private void Update()
        {
            MoveToPlayer();
        }

        public void Initialize(Transform player, int value, Vector3 velocity)
        {
            this.player = player;
            this.value = value;
            this.velocity = velocity;       
        }

        private void MoveToPlayer()
        {
            Vector3 targetDirection = player.position - transform.position;
            float distance = targetDirection.magnitude;
            velocity = Vector3.MoveTowards(velocity, targetDirection.normalized * speed, acceleration * Time.deltaTime);

            if (distance <= collectDistance)
            {
                AddGold();
            }
            else if (distance <= moveToWardDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, velocity.magnitude * Time.deltaTime);
            }
            else
            {
                transform.position += velocity * Time.deltaTime;
            }
        }

        private void AddGold()
        {
            player.GetComponentInChildren<Currency>().Add(value);
            Destroy(gameObject);
        }
    }
}
