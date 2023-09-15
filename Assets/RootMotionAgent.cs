using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    public class RootMotionAgent : MyMonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent agent;

        private bool apply = false;
        public bool Apply
        {
            get => apply;
            set
            {
                apply = value;
                agent.enabled = apply;
                agent.updatePosition = !apply;
                agent.updateRotation = !apply;
            }
        }

        public override void LoadComponent()
        {
            base.LoadComponent();
            animator = GetComponent<Animator>();
            agent = GetComponentInParent<NavMeshAgent>();
        }

        private void OnAnimatorMove()
        {
            if (Apply)
            {
                Vector3 position = animator.rootPosition;
                position.y = agent.nextPosition.y;
                agent.transform.position = position;
                agent.nextPosition = transform.position;
                agent.transform.rotation *= animator.deltaRotation;
            }
        }
    }
}
