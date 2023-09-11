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

            idle.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            chase.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            attack.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            strafeForward.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            strafeBackward.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            findAttacker.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            giveUp.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            hurt.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            defeat.Initialize(animator, ai, agent, targetDetector, aiLook, this);

            AddTransition(idle, chase, () => targetDetector.HasTarget() && AiSkill.CanAttack);
            AddTransition(idle, strafeForward, () => targetDetector.HasTarget() && !AiSkill.CanAttack);

            AddTransition(chase, attack, () => targetDetector.DistanceToDestination <= AiSkill.SkillRange);
            AddTransition(chase, idle, () => !targetDetector.HasTarget());

            AddTransition(attack, strafeBackward, () => GetCurrentState().HasTransitionRequest() && targetDetector.HasTarget());
            AddTransition(attack, idle, () => GetCurrentState().HasTransitionRequest() && !targetDetector.HasTarget());

            AddTransition(strafeForward, idle, () => !targetDetector.HasTarget());
            AddTransition(strafeForward, chase, () => AiSkill.CanAttack);

            AddTransition(strafeBackward, chase, () => AiSkill.CanAttack);
            AddTransition(strafeBackward, strafeForward, () => !AiSkill.CanAttack && GetCurrentState().HasTransitionRequest());

            AddTransition(findAttacker, chase, () => targetDetector.HasTarget() && AiSkill.CanAttack);
            AddTransition(findAttacker, idle, () => findAttacker.TimeOut);
            AddTransition(findAttacker, strafeBackward, () => targetDetector.HasTarget() && !AiSkill.CanAttack);

            AddTransition(giveUp, idle, () => Vector3.Distance(transform.position, startPoint) < 0.1f);

            AddTransition(hurt, findAttacker, () => GetCurrentState().HasTransitionRequest() && !targetDetector.HasTarget());
            AddTransition(hurt, strafeBackward, () => GetCurrentState().HasTransitionRequest() && targetDetector.HasTarget() && !AiSkill.CanAttack);
            AddTransition(hurt, chase, () => GetCurrentState().HasTransitionRequest() && targetDetector.HasTarget() && AiSkill.CanAttack);

            AddTransitionFromAny(giveUp, GiveUpCondition);

            bool GiveUpCondition()
            {
                if (currentState == defeat || currentState == attack) return false;

                if (!targetDetector.HasTarget()) return false;
                return Vector3.Distance(targetDetector.Target.position, startPoint) > activeRadius;
            }
        }

        private void OnEnable()
        {
            health.HealthChanged += TransitionOntakeDamage;
            health.Defeat += OnDefeat;
        }

        private void Start() => SwitchOn(idle);

        private void OnDisable()
        {
            health.HealthChanged -= TransitionOntakeDamage;
            health.Defeat -= OnDefeat;
        }

        private void TransitionOntakeDamage(float diff)
        {
            if (currentState == defeat || currentState == attack) return;

            if (diff >= 0) return;
            SwitchState(hurt);
        }

        private void OnDefeat()
        {
            SwitchState(defeat);
        }
    }
}
