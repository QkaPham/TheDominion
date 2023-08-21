using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class PlayerStateAttack : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Attack";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0f;

        public bool IsAttackFinished { get; private set; }
        private int MaxHit => player.Weapon.EquipedWeapon.HitNumber;

        private int currentHit = 1;

        public override void Enter()
        {
            animationEvent.AttackFinish += OnAttackFinish;
            currentHit = 1;
            IsAttackFinished = false;
            player.RotateToClosetEnemy();
            player.ApplyRootMotion(true);
            player.WeaponPlacing.LoadWeapon(WeaponPosition.RightHand);
            animator.SetFloat(StateHash, currentHit);
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            animationEvent.AttackFinish -= OnAttackFinish;
            player.ApplyRootMotion(false);
            player.WeaponPlacing.LoadWeapon(WeaponPosition.Back);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsAttackFinished && input.Attack)
            {
                input.HasAttackBuffer = true;
            }

            if (IsAttackFinished && (input.Attack || input.HasAttackBuffer))
            {
                ComboAttack();
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        private void ComboAttack()
        {
            player.RotateToClosetEnemy();
            input.HasAttackBuffer = false;
            currentHit = currentHit % MaxHit + 1;
            animator.CrossFade(StateHash, TransitionDuration, 0, 0);
            animator.SetFloat(StateHash, currentHit);
            IsAttackFinished = false;
        }

        private void OnAttackFinish() => IsAttackFinished = true;
    }
}