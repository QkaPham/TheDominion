using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class AIStateMachine : StateMachine
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected AIController ai;
        [field: SerializeField] public AISkills AiSkill { get; protected set; }
        [SerializeField] protected AILook aiLook;
        [SerializeField] public Health health;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected TargetDetector targetDetector;
        [SerializeField] public Vector3 startPoint;
        [SerializeField] protected float activeRadius = 10f;

        protected virtual void Awake()
        {
            startPoint = transform.position;
        }

        public override void LoadComponent()
        {
            base.LoadComponent();
            animator = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
            ai = GetComponent<AIController>();
            health = GetComponent<Health>();
            targetDetector = GetComponent<TargetDetector>();
            AiSkill = GetComponent<AISkills>();
            aiLook = GetComponent<AILook>();
        }

        protected AIState GetCurrentState() => (AIState)currentState;
    }
}
