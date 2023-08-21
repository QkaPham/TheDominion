using UnityEngine;

namespace Project3D
{
    public class RootMotionController : MyMonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController controller;
        [SerializeField] private LayerMask groundLayer;
        public bool Apply { get; set; } = false;

        public override void LoadComponent()
        {
            base.LoadComponent();
            controller = transform.root.GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            if (Apply)
            {
                if (PreventFallingCheck())
                {
                    controller.Move(animator.deltaPosition);
                }
                transform.root.rotation *= animator.deltaRotation;
            }
        }

        private bool PreventFallingCheck()
        {
            var origin = controller.bounds.center + animator.deltaPosition;
            var maxDistance = controller.height / 2 + controller.stepOffset + 0.1f;
            return Physics.Raycast(origin, Vector3.down, maxDistance, groundLayer);
        }
    }
}
