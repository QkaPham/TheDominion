using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Project3D
{
    public class RootMotionController : MyMonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController controller;
        [SerializeField] private LayerMask groundLayer;
        public bool Apply { get; set; } = false;
        public bool PreventFalling { get; set; } = true;
        public float Speed { get; set; } = 1f;

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
                controller.transform.rotation *= animator.deltaRotation;
                if (!PreventFalling)
                {
                    controller.Move(animator.deltaPosition * Speed);
                    return;
                }

                if (PreventFallingCheck())
                {
                    controller.Move(animator.deltaPosition * Speed);
                }
            }
        }

        public void ApplyRootMotion(bool apply, float speed, bool preventFalling)
        {
            Apply = apply;
            Speed = speed;
            PreventFalling = preventFalling;
        }

        private bool PreventFallingCheck()
        {
            var origin = controller.bounds.center + animator.deltaPosition;
            var maxDistance = controller.height / 2 + controller.stepOffset + 0.1f;
            return Physics.Raycast(origin, Vector3.down, maxDistance, groundLayer);
        }
    }
}
