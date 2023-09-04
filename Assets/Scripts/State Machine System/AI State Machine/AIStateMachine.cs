using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class AIStateMachine : StateMachine
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected AIController ai;
        [SerializeField] protected AISkills aiSkill;
        [SerializeField] public Health health;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected TargetDetector targetDetector;
        [SerializeField] public Vector3 startPoint;
        [SerializeField] protected float activeRadius = 10f;
        public int SkillHash => aiSkill.NextSkill.Hash;
        public string SkillName => aiSkill.NextSkill.Name;

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
            aiSkill = GetComponent<AISkills>();
        }

        protected AIState GetCurrentState() => (AIState)currentState;

        public virtual void UseSkill() => aiSkill.Activate(agent);
    }
}
