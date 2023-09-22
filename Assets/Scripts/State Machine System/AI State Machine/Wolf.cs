using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class Wolf : AIStateMachine
    {
        [SerializeField] protected AIStateIdle idle;
        [SerializeField] protected AIStateAttack attack;
        [SerializeField] protected AIStateChase chase;
        [SerializeField] protected AIStateStrafeForward strafeForward;
        [SerializeField] protected AIStateStrafeBackward strafeBackward;
        [SerializeField] protected AIStateFindAttacker findAttacker;
        [SerializeField] protected AIStateGiveUp giveUp;
        [SerializeField] protected AIStateHurt hurt;
        [SerializeField] protected AIStateDefeat defeat;

        protected override void Awake()
        {
            base.Awake();

            idle.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            chase.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            attack.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            strafeForward.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            strafeBackward.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            findAttacker.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            giveUp.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            hurt.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);
            defeat.Initialize(Animator, RootMotionAgent, Agent, TargetDetector, AiLook, this);

            AddTransition(idle, chase, () => TargetDetector.HasTarget() && AiSkill.IsCoolDownFinish);
            AddTransition(idle, strafeForward, () => TargetDetector.HasTarget() && !AiSkill.IsCoolDownFinish);

            AddTransition(chase, attack, () => TargetDetector.DistanceToDestination <= AiSkill.SkillRange);
            AddTransition(chase, idle, () => !TargetDetector.HasTarget());

            AddTransition(attack, strafeBackward, () => GetCurrentState().HasTransitionRequest() && TargetDetector.HasTarget());
            AddTransition(attack, idle, () => GetCurrentState().HasTransitionRequest() && !TargetDetector.HasTarget());

            AddTransition(strafeForward, idle, () => !TargetDetector.HasTarget());
            AddTransition(strafeForward, chase, () => AiSkill.IsCoolDownFinish);

            AddTransition(strafeBackward, chase, () => AiSkill.IsCoolDownFinish);
            AddTransition(strafeBackward, strafeForward, () => !AiSkill.IsCoolDownFinish && GetCurrentState().HasTransitionRequest());

            AddTransition(findAttacker, chase, () => TargetDetector.HasTarget() && AiSkill.IsCoolDownFinish);
            AddTransition(findAttacker, idle, () => findAttacker.TimeOut);
            AddTransition(findAttacker, strafeBackward, () => TargetDetector.HasTarget() && !AiSkill.IsCoolDownFinish);

            AddTransition(giveUp, idle, () => Vector3.Distance(transform.position, startPoint) < 0.1f);

            AddTransition(hurt, findAttacker, () => GetCurrentState().HasTransitionRequest() && !TargetDetector.HasTarget());
            AddTransition(hurt, strafeBackward, () => GetCurrentState().HasTransitionRequest() && TargetDetector.HasTarget() && !AiSkill.IsCoolDownFinish);
            AddTransition(hurt, chase, () => GetCurrentState().HasTransitionRequest() && TargetDetector.HasTarget() && AiSkill.IsCoolDownFinish);

            AddTransitionFromAny(giveUp, GiveUpCondition);

            bool GiveUpCondition()
            {
                if (currentState == defeat || currentState == attack) return false;

                if (!TargetDetector.HasTarget()) return false;
                return Vector3.Distance(TargetDetector.Target.position, startPoint) > activeRadius;
            }
        }

        private void OnEnable()
        {
            Health.HealthChanged += TransitionOntakeDamage;
            Health.Defeat += OnDefeat;
        }

        private void Start() => SwitchOn(idle);

        private void OnDisable()
        {
            Health.HealthChanged -= TransitionOntakeDamage;
            Health.Defeat -= OnDefeat;
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
