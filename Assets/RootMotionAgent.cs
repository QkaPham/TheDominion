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

        private bool isApply = false;
        public bool IsApply
        {
            get => isApply;
            set
            {
                isApply = value;
                agent.enabled = isApply;
                agent.updatePosition = !isApply;
                agent.updateRotation = !isApply;
            }
        }

        [field: SerializeField] public float PositionMultiply { get; private set; } = 1f;
        [field: SerializeField] public float RotationMultiply { get; private set; } = 1f;

        public override void LoadComponent()
        {
            base.LoadComponent();
            animator = GetComponent<Animator>();
            agent = GetComponentInParent<NavMeshAgent>();
        }

        private void OnAnimatorMove()
        {
            if (IsApply)
            {
                Vector3 position = animator.rootPosition * PositionMultiply;
                position.y = agent.nextPosition.y;
                agent.transform.position = position;
                agent.nextPosition = transform.position;

                var nextRotation = agent.transform.rotation * animator.deltaRotation;
                agent.transform.rotation = Quaternion.LerpUnclamped(agent.transform.rotation, nextRotation, RotationMultiply);
            }
        }

        public void Apply(bool isApply, float positionMultiply = 1f, float rotationMultiply = 1f)
        {
            IsApply = isApply;
            PositionMultiply = positionMultiply;
            RotationMultiply = rotationMultiply;
        }
    }
}
