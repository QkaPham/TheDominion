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

        public override void LoadComponent()
        {
            base.LoadComponent();
            animator = GetComponentInChildren<Animator>();
            ai = GetComponent<AIController>();
            health = GetComponent<Health>();
            targetDetector = GetComponent<TargetDetector>();
            aiSkill = GetComponent<AISkills>();
        }

        protected override void Awake()
        {
            base.Awake();

            idle.Initialize(animator, ai, agent, targetDetector, this);
            chase.Initialize(animator, ai, agent, targetDetector, this);
            attack.Initialize(animator, ai, agent, targetDetector, this);
            strafeForward.Initialize(animator, ai, agent, targetDetector, this);
            strafeBackward.Initialize(animator, ai, agent, targetDetector, this);
            findAttacker.Initialize(animator, ai, agent, targetDetector, this);
            giveUp.Initialize(animator, ai, agent, targetDetector, this);
            hurt.Initialize(animator, ai, agent, targetDetector, this);
            defeat.Initialize(animator, ai, agent, targetDetector, this);

            AddTransition(idle, chase, () => targetDetector.HasTarget() && aiSkill.CanAttack);
            AddTransition(idle, strafeForward, () => targetDetector.HasTarget() && !aiSkill.CanAttack);

            AddTransition(chase, attack, () => targetDetector.DistanceToTarget <= aiSkill.SkillRange);
            AddTransition(chase, idle, () => !targetDetector.HasTarget());

            AddTransition(attack, strafeBackward, () => GetCurrentState().HasTransitionRequest() && targetDetector.HasTarget());
            AddTransition(attack, idle, () => GetCurrentState().HasTransitionRequest() && !targetDetector.HasTarget());

            AddTransition(strafeForward, idle, () => !targetDetector.HasTarget());
            AddTransition(strafeForward, chase, () => aiSkill.CanAttack);

            AddTransition(strafeBackward, chase, () => aiSkill.CanAttack);
            AddTransition(strafeBackward, strafeForward, () => !aiSkill.CanAttack && GetCurrentState().HasTransitionRequest());

            AddTransition(findAttacker, chase, () => targetDetector.HasTarget() && aiSkill.CanAttack);
            AddTransition(findAttacker, idle, () => findAttacker.TimeOut);
            AddTransition(findAttacker, strafeBackward, () => targetDetector.HasTarget() && !aiSkill.CanAttack);

            AddTransition(giveUp, idle, () => Vector3.Distance(transform.position, startPoint) < 0.1f);

            AddTransition(hurt, findAttacker, () => GetCurrentState().HasTransitionRequest() && !targetDetector.HasTarget());
            AddTransition(hurt, strafeBackward, () => GetCurrentState().HasTransitionRequest() && targetDetector.HasTarget() && !aiSkill.CanAttack);
            AddTransition(hurt, chase, () => GetCurrentState().HasTransitionRequest() && targetDetector.HasTarget() && aiSkill.CanAttack);

            AddTransitionFromAny(giveUp, GiveUpCondition);

            bool GiveUpCondition()
            {
                if (currentState == defeat || currentState == attack) return false;

                if (!targetDetector.HasTarget()) return false;
                return Vector3.Distance(targetDetector.Target.position, startPoint) > activeRadius;
            }
        }

        private void Start()
        {
            SwitchOn(idle);

            health.HealthChanged += TransitionOntakeDamage;
            health.Defeat += OnDefeat;
        }


        private void OnDestroy()
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
