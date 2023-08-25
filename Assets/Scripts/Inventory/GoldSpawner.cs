using UnityEngine;

namespace Project3D
{
    public class GoldSpawner : MyMonoBehaviour
    {
        [SerializeField]private Health health;
        
        [SerializeField] private Gold goldPrefab;
        [SerializeField] private Transform playerTransform;

        public override void LoadComponent()
        {
            base.LoadComponent();
            health = GetComponent<Health>();
        }

        private void Awake()
        {
            playerTransform = FindObjectOfType<PlayerController>().transform;
        }

        private void OnEnable() => health.Defeat += OnDefeate;
        private void OnDisable() => health.Defeat -= OnDefeate;

        private void OnDefeate() => CreateGold(5, Vector3.up);

        public void CreateGold(int value, Vector3 velocity)
        {
            var gold = Instantiate(goldPrefab, transform.position, Quaternion.identity, transform);
            gold.Initialize(playerTransform, value, velocity);
        }
    }
}
