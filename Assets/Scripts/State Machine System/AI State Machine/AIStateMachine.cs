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
        [SerializeField] protected AIStateStrafe strafe;
        [SerializeField] protected AIStateStrafeBack strafeBack;
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
            strafe.Initialize(animator, ai, agent, targetDetector, this);
            strafeBack.Initialize(animator, ai, agent, targetDetector, this);
            findAttacker.Initialize(animator, ai, agent, targetDetector, this);
            defeat.Initialize(animator, ai, agent, targetDetector, this);
            giveUp.Initialize(animator, ai, agent, targetDetector, this);

            AddTransition(idle, chase, () => targetDetector.HasTarget() && ai.CanAttack);
            AddTransition(idle, strafe, () => targetDetector.HasTarget() && !ai.CanAttack);

            AddTransition(attack, strafeBack, () => attack.IsAnimationFinished && targetDetector.HasTarget());
            AddTransition(attack, idle, () => attack.IsAnimationFinished && !targetDetector.HasTarget());

            AddTransition(chase, attack, chase.ReachAttackRange);
            AddTransition(chase, idle, () => !targetDetector.HasTarget());

            AddTransition(strafe, idle, () => !targetDetector.HasTarget());
            AddTransition(strafe, chase, () => ai.CanAttack);

            AddTransition(strafeBack, attack, () => strafeBack.ReachDesireDistance && ai.CanAttack);
            AddTransition(strafeBack, strafe, () => strafeBack.ReachDesireDistance && !ai.CanAttack);

            AddTransition(findAttacker, chase, () => findAttacker.StiffTimeOut && targetDetector.HasTarget());
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
            if (!targetDetector.HasTarget() && diff < 0)
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
