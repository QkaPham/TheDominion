using UnityEngine;

namespace Project3D
{
    public class Wolf : AIStateMachine
    {
        [SerializeField] protected AIStateIdle idle;
        [SerializeField] protected AIStateAttack attack;
        [SerializeField] protected AIStateChase chase;
        [SerializeField] protected AIStateStrafeForward strafeForward;
        [SerializeField] protected AIStateStrafeBackward strafeBack;
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
        }

        private void Awake()
        {
            idle.Initialize(animator, ai, agent, targetDetector, this);
            attack.Initialize(animator, ai, agent, targetDetector, this);
            chase.Initialize(animator, ai, agent, targetDetector, this);
            strafeForward.Initialize(animator, ai, agent, targetDetector, this);
            strafeBack.Initialize(animator, ai, agent, targetDetector, this);
            findAttacker.Initialize(animator, ai, agent, targetDetector, this);
            giveUp.Initialize(animator, ai, agent, targetDetector, this);
            hurt.Initialize(animator, ai, agent, targetDetector, this);
            defeat.Initialize(animator, ai, agent, targetDetector, this);

            AddTransition(idle, chase, () => targetDetector.HasTarget() && ai.CanAttack);
            AddTransition(idle, strafeForward, () => targetDetector.HasTarget() && !ai.CanAttack);

            AddTransition(attack, strafeBack, () => attack.IsAnimationFinished && targetDetector.HasTarget());
            AddTransition(attack, idle, () => attack.IsAnimationFinished && !targetDetector.HasTarget());

            AddTransition(chase, attack, chase.ReachAttackRange);
            AddTransition(chase, idle, () => !targetDetector.HasTarget());

            AddTransition(strafeForward, idle, () => !targetDetector.HasTarget());
            AddTransition(strafeForward, chase, () => ai.CanAttack);

            AddTransition(strafeBack, attack, () => strafeBack.ReachDesireDistance && ai.CanAttack);
            AddTransition(strafeBack, strafeForward, () => strafeBack.ReachDesireDistance && !ai.CanAttack);

            AddTransition(findAttacker, chase, () => targetDetector.HasTarget() && ai.CanAttack);
            AddTransition(findAttacker, idle, () => findAttacker.TimeOut);
            AddTransition(findAttacker, strafeBack, () => targetDetector.HasTarget() && !ai.CanAttack);

            AddTransition(giveUp, idle, () => Vector3.Distance(transform.position, startPoint.position) < 0.1f);

            AddTransition(hurt, findAttacker, () => hurt.IsAnimationFinished && !targetDetector.HasTarget());
            AddTransition(hurt, strafeBack, () => hurt.IsAnimationFinished && targetDetector.HasTarget() && !ai.CanAttack);
            AddTransition(hurt, chase, () => hurt.IsAnimationFinished && targetDetector.HasTarget() && ai.CanAttack);

            AddTransitionFromAny(giveUp, GiveUpCondition);

            bool GiveUpCondition()
            {
                if (!targetDetector.HasTarget()) return false;
                return Vector3.Distance(targetDetector.Target.position, startPoint.position) > activeRadius;
            }
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
            if (diff < 0)
            {
                SwitchState(hurt);
            }
        }

        private void OnDefeat()
        {
            SwitchState(defeat);
        }
    }
}
