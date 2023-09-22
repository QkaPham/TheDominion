using System;
using System.Collections;
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
        [SerializeField] private Transform aimPoint;

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
            fireBreathEvent.FireBreathStart += StartShootForward;
            fireBreathEvent.FireBreathEnd += StopShoot;
            fireBreathEvent.SpecialStart += StartShootToPoint;
            fireBreathEvent.SpecialEnd += StopShoot;
        }

        private void OnDisable()
        {
            fireBreathEvent.FireBreathStart -= StartShootForward;
            fireBreathEvent.FireBreathEnd -= StopShoot;
            fireBreathEvent.SpecialStart -= StartShootToPoint;
            fireBreathEvent.SpecialEnd -= StopShoot;
        }

        private void StartShootForward()
        {
            laserBeamFx.enabled = true;
            flashFx.Play();
            StartCoroutine(ShootForward());
        }

        private void StartShootToPoint()
        {
            laserBeamFx.enabled = true;
            flashFx.Play();
            StartCoroutine(ShootToPoint(aimPoint));
        }

        private void StopShoot()
        {
            laserBeamFx.enabled = false;
            flashFx.Stop();
            foreach (var hitFx in hitFxs)
            {
                hitFx.Stop();
            }
            StopAllCoroutines();
        }

        public IEnumerator ShootForward()
        {
            while (true)
            {
                var fwd = beamHolder.forward;
                fwd.y = 0;
                Shoot(fwd, distance);
                yield return null;
            }
        }

        public IEnumerator ShootToPoint(Transform point)
        {
            while (true)
            {
                var dir = point.position - beamHolder.position;
                Shoot(dir, 256f);
                yield return null;
            }
        }

        private void Shoot(Vector3 direction, float distance)
        {
            if (Physics.SphereCast(transform.position, radius, direction, out var hitInfo, distance, targetLayer))
            {
                hitFxTransform.transform.position = hitInfo.point;
                hitFxTransform.forward = hitInfo.normal;
                laserBeamFx.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));

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
                laserBeamFx.SetPosition(1, transform.InverseTransformPoint(transform.position + direction * distance));
                foreach (var hitFx in hitFxs)
                {
                    hitFx.Stop();
                }
            }
        }
    }
}
