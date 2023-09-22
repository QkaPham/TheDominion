using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class AIStateMachine : StateMachine
    {
        [field: SerializeField] public Animator Animator { get; protected set; }
        [field: SerializeField] public RootMotionAgent RootMotionAgent { get; protected set; }
        [field: SerializeField] public AISkills AiSkill { get; protected set; }
        [field: SerializeField] public AILook AiLook { get; protected set; }
        [field: SerializeField] public Health Health { get; protected set; }
        [field: SerializeField] public NavMeshAgent Agent { get; protected set; }
        [field: SerializeField] public TargetDetector TargetDetector { get; protected set; }
        [SerializeField] public Vector3 startPoint;
        [SerializeField] protected float activeRadius = 10f;

        protected virtual void Awake()
        {
            startPoint = transform.position;
        }

        public override void LoadComponent()
        {
            base.LoadComponent();
            Animator = GetComponentInChildren<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            RootMotionAgent = GetComponent<RootMotionAgent>();
            Health = GetComponent<Health>();
            TargetDetector = GetComponent<TargetDetector>();
            AiSkill = GetComponent<AISkills>();
            AiLook = GetComponent<AILook>();
        }

        protected AIState GetCurrentState() => (AIState)currentState;

        public void ReStart()
        {
            transform.position = startPoint;
            Health.HealFull();
            TargetDetector.Target = null;
        }
    }
}
