using System;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class AIStateMachine : StateMachine
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AIController ai;
        [SerializeField] private Health health;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private TargetDetector targetDetector;
        [SerializeField] public Transform startPoint;
        [SerializeField] private float activeRadius = 10f;

        [SerializeField] protected AIStateIdle idle;
        [SerializeField] protected AIStateAttack attack;
        [SerializeField] protected AIStateChase chase;
        [SerializeField] protected AIStateAwaitCooldown awaitCooldown;
        [SerializeField] protected AIStateStepBack stepBack;
        [SerializeField] protected AIStateFindAttacker findAttacker;
        [SerializeField] protected AIStateDefeat defeat;
        [SerializeField] protected AIStateGiveUp giveUp;

        public override void LoadComponent()
        {
            base.LoadComponent();
            animator = GetComponentInChildren<Animator>();
            ai = GetComponent<AIController>();
            health = GetComponent<Health>();
            targetDetector = GetComponent<TargetDetector>();
        }

        private void Awake()
        {
            idle.Initialize(animator, ai, agent, targetDetector, this);
            attack.Initialize(animator, ai, agent, targetDetector, this);
            chase.Initialize(animator, ai, agent, targetDetector, this);
            awaitCooldown.Initialize(animator, ai, agent, targetDetector, this);
            stepBack.Initialize(animator, ai, agent, targetDetector, this);
            findAttacker.Initialize(animator, ai, agent, targetDetector, this);
            defeat.Initialize(animator, ai, agent, targetDetector, this);
            giveUp.Initialize(animator, ai, agent, targetDetector, this);

            AddTransition(idle, chase, () => targetDetector.HasTargetForward() && ai.CanAttack);
            AddTransition(idle, awaitCooldown, () => targetDetector.HasTargetForward() && !ai.CanAttack);

            AddTransition(attack, stepBack, () => attack.IsAnimationFinished && targetDetector.HasTargetInRadius());
            AddTransition(attack, idle, () => attack.IsAnimationFinished && !targetDetector.HasTargetInRadius());

            AddTransition(chase, attack, chase.ReachAttackRange);
            AddTransition(chase, idle, () => !targetDetector.HasTargetInRadius());

            AddTransition(awaitCooldown, idle, () => !targetDetector.HasTargetInRadius());
            AddTransition(awaitCooldown, chase, () => ai.CanAttack);

            AddTransition(stepBack, attack, () => targetDetector.DistanceToTarget > stepBack.minDistance && ai.CanAttack);
            AddTransition(stepBack, awaitCooldown, () => targetDetector.DistanceToTarget > stepBack.minDistance && !ai.CanAttack);

            AddTransition(findAttacker, chase, () => findAttacker.StiffTimeOut && targetDetector.HasTargetInRadius());
            AddTransition(findAttacker, idle, () => findAttacker.TimeOut);

            AddTransition(giveUp, idle, () => Vector3.Distance(transform.position, startPoint.position) < 0.1f);

            AddTransitionFromAny(giveUp, () => Vector3.Distance(transform.position, startPoint.position) > activeRadius);
        }

        private void Start()
        {
            SwitchOn(idle);

            health.HealthChanged += TransitionOntakeDamage;
            health.Defeated += OnDefeat;
        }


        private void OnDestroy()
        {
            health.HealthChanged -= TransitionOntakeDamage;
            health.Defeated -= OnDefeat;
        }

        private void TransitionOntakeDamage(float diff)
        {
            if (!targetDetector.HasTargetInRadius() && diff < 0)
            {
                SwitchState(findAttacker);
            }
        }

        private void OnDefeat()
        {
            SwitchState(defeat);
        }
    }
}
