using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class AIStateMachine : StateMachine
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected AIController ai;
        [SerializeField] public AISkills aiSkill;
        [SerializeField] public Health health;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected TargetDetector targetDetector;
        [SerializeField] public Transform startPoint;
        [SerializeField] protected float activeRadius = 10f;

        public override void LoadComponent()
        {
            base.LoadComponent();
            animator = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
            ai = GetComponent<AIController>();
            health = GetComponent<Health>();
            targetDetector = GetComponent<TargetDetector>();
        }

        public virtual void UseSkill()
        {
            aiSkill.Activate(agent);
        }
    }
}
