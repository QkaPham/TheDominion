using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class Dragon : AIStateMachine
    {
        [SerializeField] private AIStateIdle idle;
        [SerializeField] private AIStateChase chase;
        [SerializeField] private AIStateAttack attack;
        [SerializeField] private AIStateHurt hurt;
        [SerializeField] private AIStateDefeat defeat;

        protected override void Awake()
        {
            base.Awake();

            idle.Initialize(animator, ai, agent, targetDetector, this);
            chase.Initialize(animator, ai, agent, targetDetector, this);
            attack.Initialize(animator, ai, agent, targetDetector, this);
            hurt.Initialize(animator, ai, agent, targetDetector, this);
            defeat.Initialize(animator, ai, agent, targetDetector, this);

            AddTransition(idle, chase, () => targetDetector.HasTarget() && aiSkill.CanAttack);

            AddTransition(chase, attack, () => targetDetector.DistanceToDestination <= aiSkill.SkillRange);
            AddTransition(chase, idle, () => !targetDetector.HasTarget());

            AddTransition(attack, idle, () => GetCurrentState().HasTransitionRequest());
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
