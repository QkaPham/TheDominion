using TMPro;
using UnityEngine;

namespace Project3D
{
    public class DamagePopup : MyMonoBehaviour
    {
        [SerializeField] private float lifeTime = 2f;
        [SerializeField] private TMP_Text damageText;

        [SerializeField] private Vector2 initialVelocity;
        [SerializeField] private float acceleration;

        private Vector3 velocity = Vector3.zero;

        private void Start()
        {
            velocity = initialVelocity.y * transform.up + Random.Range(-initialVelocity.x, initialVelocity.x) * transform.right;
        }

        private void Update()
        {
            velocity.y += Time.deltaTime * acceleration;
            transform.localPosition += Time.deltaTime * velocity;
        }

        public void Initialize(float damage)
        {
            Destroy(gameObject, lifeTime);
            damageText.text = string.Format("{0:0}", damage);
        }
    }
}
