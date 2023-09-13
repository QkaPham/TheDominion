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
        public override bool HasRequestTransition() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(StateName) && !animator.IsInTransition(0);

        public override void Enter()
        {
            animationEvent.AttackFinish += OnAttackFinish;
            currentHit = 1;
            IsAttackFinished = false;
            player.RotateToClosetEnemy();
            player.ApplyRootMotion(true);
            player.WeaponPlacing.LoadWeaponToHand();
            animator.SetFloat(StateHash, currentHit);
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            animationEvent.AttackFinish -= OnAttackFinish;
            player.ApplyRootMotion(false);
            player.WeaponPlacing.LoadWeaponToBack();
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

        private void OnAttackFinish()
        {
            IsAttackFinished = true;
        }
    }

    [Serializable]
    public class PlayerStateAttackSeperate : PlayerState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Attack";
        [field: SerializeField] protected override float TransitionDuration { get; set; } = 0f;

        public bool IsAttackFinished { get; private set; }
        private int MaxHit => player.Weapon.EquipedWeapon.HitNumber;

        private int currentHit = 1;

        private int[] StateHashs;
        protected override int StateHash => StateHashs[currentHit];

        public override bool HasRequestTransition() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(StateName + currentHit) && !animator.IsInTransition(0);

        public override void Initialize(Animator animator, PlayerController player, PlayerInput input, PlayerAnimationEvent animationEvent, PlayerStateMachine stateMachine)
        {
            base.Initialize(animator, player, input, animationEvent, stateMachine);

            StateHashs = new int[10];
            for (int i = 0; i < StateHashs.Length; i++)
            {
                StateHashs[i] = Animator.StringToHash(StateName + i);
            }
        }

        public override void Enter()
        {
            animationEvent.AttackFinish += OnAttackFinish;
            currentHit = 1;
            IsAttackFinished = false;
            player.RotateToClosetEnemy();
            player.ApplyRootMotion(true);
            player.WeaponPlacing.LoadWeaponToHand();
            animator.CrossFade(StateHash, TransitionDuration);
            stateStartTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            animationEvent.AttackFinish -= OnAttackFinish;
            player.ApplyRootMotion(false);
            player.WeaponPlacing.LoadWeaponToBack();
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
            animator.CrossFade(StateHash, TransitionDuration);
            IsAttackFinished = false;
        }

        private void OnAttackFinish()
        {
            IsAttackFinished = true;
        }
    }
}