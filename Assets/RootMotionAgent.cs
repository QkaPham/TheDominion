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
        public bool Apply { get; set; } = true;

        public override void LoadComponent()
        {
            base.LoadComponent();
            animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            if (Apply)
            {
                agent.enabled = true;
                agent.Move(animator.deltaPosition);
                agent.transform.rotation *= animator.deltaRotation;
            }
        }
    }
}
