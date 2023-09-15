using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class Dragon : AIStateMachine
    {
        [SerializeField] private AIStateIdle idle;
        //[SerializeField] private AIStateChase chase;
        [SerializeField] private AIStateAttack attack;
        [SerializeField] private AIStateRotateToTarget chase;
        [SerializeField] private AIStateHurt hurt;
        [SerializeField] private AIStateDefeat defeat;

        protected override void Awake()
        {
            base.Awake();

            idle.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            chase.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            attack.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            hurt.Initialize(animator, ai, agent, targetDetector, aiLook, this);
            defeat.Initialize(animator, ai, agent, targetDetector, aiLook, this);

            AddTransition(idle, chase, () => targetDetector.HasTarget() && AiSkill.CanAttack);


            AddTransition(chase, attack, () => GetCurrentState().HasTransitionRequest() && targetDetector.DistanceToDestination <= AiSkill.SkillRange );
           // AddTransition(chase, rotate, () => targetDetector.DistanceToDestination <= AiSkill.SkillRange);
            AddTransition(chase, idle, () => !targetDetector.HasTarget());
            AddTransition(attack, idle, () => GetCurrentState().HasTransitionRequest());

            //AddTransition(rotate, attack, () => GetCurrentState().HasTransitionRequest());

            AddTransition(hurt, idle, () => GetCurrentState().HasTransitionRequest() && targetDetector.HasTarget() && !AiSkill.CanAttack);
            AddTransition(hurt, chase, () => GetCurrentState().HasTransitionRequest() && targetDetector.HasTarget() && AiSkill.CanAttack);
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
