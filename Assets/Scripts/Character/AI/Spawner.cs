using UnityEngine;

namespace Project3D
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        private bool Alive = false;

        private void OnEnable()
        {
            CheckPoint.InteractEvent += Spawn;
        }

        private void OnDisable()
        {
            CheckPoint.InteractEvent -= Spawn;
        }

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            if (!Alive)
            {
                var enemy = Instantiate(enemyPrefab, transform);
                enemy.GetComponentInChildren<Health>().Defeat += () => Alive = false;
                Alive = true;
            }
        }
    }
}
