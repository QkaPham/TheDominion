using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class Dragon : AIStateMachine
    {
        [SerializeField] private AIStateIdleRootMotion idle;
        [SerializeField] private AIStateChaseRootMotion chase;
        [SerializeField] private AIStateAttackRootMotion attack;
        [SerializeField] private AIStateRotateRootMotion rotate180;
        [SerializeField] private AIStateHurt hurt;
        [SerializeField] private AIStateDefeat defeat;

        protected override void Awake()
        {
            base.Awake();
            idle.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            chase.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            rotate180.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            attack.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            hurt.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            defeat.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);

            AddTransition(idle, rotate180, () => TargetDetector.HasTarget() && !AiSkill.HasCanUseSkill() && Mathf.Abs(TargetDetector.SignedAngleToTarget()) >= 135f);

            AddTransition(idle, chase, () => TargetDetector.HasTarget() && AiSkill.IsCoolDownFinish && !AiSkill.HasCanUseSkill());
            AddTransition(idle, attack, () => TargetDetector.HasTarget() && AiSkill.IsCoolDownFinish && AiSkill.HasCanUseSkill());

            AddTransition(rotate180, idle, () => GetCurrentState().HasTransitionRequest());

            AddTransition(chase, rotate180, () => Mathf.Abs(TargetDetector.SignedAngleToTarget()) >= 135f);
            AddTransition(chase, idle, () => !TargetDetector.HasTarget());
            AddTransition(chase, attack, () => TargetDetector.HasTarget() && AiSkill.HasCanUseSkill());

            AddTransition(attack, idle, () => GetCurrentState().HasTransitionRequest());

            AddTransition(hurt, idle, () => GetCurrentState().HasTransitionRequest() && !TargetDetector.HasTarget());
            AddTransition(hurt, attack, () => GetCurrentState().HasTransitionRequest() && TargetDetector.HasTarget() && AiSkill.IsCoolDownFinish);
            AddTransition(hurt, chase, () => GetCurrentState().HasTransitionRequest() && TargetDetector.HasTarget() && !AiSkill.IsCoolDownFinish);
        }

        private void OnEnable()
        {
            Health.HealthChanged += TransitionOntakeDamage;
            Health.Defeat += OnDefeat;
            PlayerStateMachine.PlayerRevive += ReStart;
        }

        private void Start() => SwitchOn(idle);

        private void OnDisable()
        {
            Health.HealthChanged -= TransitionOntakeDamage;
            Health.Defeat -= OnDefeat;
            PlayerStateMachine.PlayerRevive -= ReStart;
        }

        private void TransitionOntakeDamage(float diff)
        {
            if (currentState == defeat || currentState == attack || currentState == hurt) return;

            if (diff >= 0) return;
            SwitchState(hurt);
        }

        private void OnDefeat()
        {
            SwitchState(defeat);
        }
    }
}
