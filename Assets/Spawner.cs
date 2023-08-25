using UnityEngine;

namespace Project3D
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        private bool Alive = false;

        private void Start()
        {
            Spawn();
            CheckPoint.InteractEvent += Spawn;
        }

        private void OnDestroy()
        {
            CheckPoint.InteractEvent -= Spawn;
        }

        public void Spawn()
        {
            if (!Alive)
            {
                var enemy = Instantiate(enemyPrefab, transform);
                enemy.GetComponent<Health>().Defeat += () => Alive = false;
                Alive = true;
            }
        }
    }
}
