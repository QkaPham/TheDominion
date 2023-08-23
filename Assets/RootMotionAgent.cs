using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class RootMotionAgent : MyMonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;

        public override void LoadComponent()
        {
            base.LoadComponent();

            agent = GetComponentInParent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            animator.applyRootMotion = true;
            agent.updatePosition = false;
        }

        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            agent.transform.position = position;
            agent.nextPosition = position;
        }
    }
}
