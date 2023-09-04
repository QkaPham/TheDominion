using UnityEngine;
using UnityEngine.InputSystem;

namespace Project3D
{
    public class LaserBeam : MyMonoBehaviour
    {
        [SerializeField] private ParticleSystem flash;
        [SerializeField] private LineRenderer laserBeam;
        [SerializeField] private Transform hitFxTransform;
        [SerializeField] private float distance;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float timeBetweenDealingDamage;
        [SerializeField] private float damage;

        private ParticleSystem[] hitFxs;
        private bool enable;
        private float lastDealingDamageTime;

        public override void LoadComponent()
        {
            base.LoadComponent();

            laserBeam = GetComponent<LineRenderer>();
            hitFxTransform = transform.Find("Hit");
        }

        private void Awake()
        {
            hitFxs = GetComponentsInChildren<ParticleSystem>();
        }

        private void Update()
        {
            if (Keyboard.current.uKey.wasPressedThisFrame)
            {
                Toggle();
            }

            if (enable)
            {
                Shoot();
            }
        }

        public void Toggle()
        {
            laserBeam.enabled = !laserBeam.enabled;
            enable = !enable;
            if (enable)
            {
                flash.Play();
            }
            else
            {
                flash.Stop();
                foreach (var hitFx in hitFxs)
                {
                    hitFx.Stop();
                }
            }
        }

        private void Shoot()
        {
            //laserBeam.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, distance, targetLayer))
            {

                //laserBeam.SetPosition(1, hitInfo.point);

                hitFxTransform.transform.position = hitInfo.point;
                hitFxTransform.forward = hitInfo.normal;

                foreach (var hitFx in hitFxs)
                {
                    hitFx.Play();
                }

                if (Time.time - lastDealingDamageTime > timeBetweenDealingDamage)
                {
                    var health = hitInfo.collider.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(damage);
                    }
                    lastDealingDamageTime = Time.time;
                }

            }
            else
            {
                //laserBeam.SetPosition(1, )
                //laserBeam.SetPosition(1, transform.position + transform.forward * distance);

                foreach (var hitFx in hitFxs)
                {
                    hitFx.Stop();
                }
            }

        }
    }
}
