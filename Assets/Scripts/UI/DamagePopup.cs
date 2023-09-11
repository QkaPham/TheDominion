using TMPro;
using UnityEngine;

namespace Project3D
{
    public class DamagePopup : MyMonoBehaviour
    {
        [SerializeField] private float lifeTime = 2f;
        [SerializeField] private TMP_Text damageText;

        [SerializeField] private Vector3 velocity = Vector3.up;
        [SerializeField] private AnimationCurve alpha;
        [SerializeField] private AnimationCurve size;

        private float process;

        private void Update()
        {
            process = Mathf.MoveTowards(process, 1, Time.deltaTime / lifeTime);
            transform.localPosition += Time.deltaTime * velocity;
            damageText.alpha = alpha.Evaluate(process);
            transform.localScale = size.Evaluate(process) * Vector3.one;
        }

        public void Initialize(float damage)
        {
            Destroy(gameObject, lifeTime);
            damageText.text = string.Format("{0:0}", damage);
        }
    }
}
