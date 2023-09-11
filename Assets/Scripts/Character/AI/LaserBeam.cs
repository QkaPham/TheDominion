using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project3D
{
    public class LaserBeam : MyMonoBehaviour
    {
        [SerializeField] private LineRenderer laserBeamFx;
        [SerializeField] private ParticleSystem flashFx;
        [SerializeField] private Transform hitFxTransform;
        [SerializeField] private ParticleSystem[] hitFxs;
        [SerializeField] private float distance;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float timeBetweenDealingDamage;
        [SerializeField] private float damage;
        [SerializeField] private float radius;
        [SerializeField] private AnimationEventFireBreath fireBreathEvent;
        [SerializeField] private Transform beamHolder;

        public bool enable;
        private float lastDealingDamageTime;



        public override void LoadComponent()
        {
            base.LoadComponent();

            laserBeamFx = GetComponent<LineRenderer>();
            flashFx = transform.Find("Flash").GetComponent<ParticleSystem>();
            hitFxTransform = transform.Find("Hit");
            hitFxs = hitFxTransform.GetComponentsInChildren<ParticleSystem>();
        }

        private void OnEnable()
        {
            fireBreathEvent.FireBreathStart += Toggle;
            fireBreathEvent.FireBreathEnd += Toggle;
        }

        private void OnDisable()
        {
            fireBreathEvent.FireBreathStart -= Toggle;
            fireBreathEvent.FireBreathEnd -= Toggle;
        }

        private void Update()
        {
            if (enable)
            {
                Shoot();
            }
        }

        public void Toggle()
        {
            enable = !enable;

            laserBeamFx.enabled = enable;
            if (enable)
            {
                flashFx.Play();
            }
            else
            {
                flashFx.Stop();
                foreach (var hitFx in hitFxs)
                {
                    hitFx.Stop();
                }
            }
        }

        private void Shoot()
        {
            if (Physics.SphereCast(transform.position, radius, beamHolder.forward, out var hitInfo, distance, targetLayer))
            {
                hitFxTransform.transform.position = hitInfo.point;
                hitFxTransform.forward = hitInfo.normal;

                foreach (var hitFx in hitFxs)
                {
                    hitFx.Play();
                }

                if (Time.time - lastDealingDamageTime > timeBetweenDealingDamage)
                {
                    var health = hitInfo.collider.GetComponent<PlayerHealth>();
                    if (health != null)
                    {
                        health.TakeDamage(damage);
                    }
                    lastDealingDamageTime = Time.time;
                }
            }
            else
            {
                foreach (var hitFx in hitFxs)
                {
                    hitFx.Stop();
                }
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, beamHolder.forward * distance);
        }
    }
}
