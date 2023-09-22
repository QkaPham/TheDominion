using UnityEngine;

namespace Project3D
{
    public class RootMotionController : MyMonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController controller;
        [SerializeField] private LayerMask groundLayer;
        public bool Apply { get; set; } = false;
        public bool PreventFalling { get; set; } = true;

        //Adjust velocity and rotation of some special animation (Roll, Dash, Skills ...)
        [field: SerializeField] public float MoveSpeed { get; set; } = 1f;
        [field: SerializeField] public float RotationSpeed { get; set; } = 1f;

        public override void LoadComponent()
        {
            base.LoadComponent();
            controller = transform.root.GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            MoveAndRotate();
        }

        public void ApplyRootMotion(bool apply, bool preventFalling, float moveSpeed, float rotationSpeed)
        {
            Apply = apply;
            MoveSpeed = moveSpeed;
            RotationSpeed = rotationSpeed;
            PreventFalling = preventFalling;
        }

        private void MoveAndRotate()
        {
            if (Apply)
            {
                var nextRotation = controller.transform.rotation * animator.deltaRotation;
                controller.transform.rotation = Quaternion.LerpUnclamped(controller.transform.rotation, nextRotation, RotationSpeed);

                if (!PreventFalling || IsNextMovingGrounded())
                {
                    controller.Move(animator.deltaPosition * MoveSpeed);
                }
            }
        }

        private bool IsNextMovingGrounded()
        {
            var origin = controller.bounds.center + animator.deltaPosition;
            var maxDistance = controller.height / 2 + controller.stepOffset + 0.1f;
            return Physics.Raycast(origin, Vector3.down, maxDistance, groundLayer);
        }
    }
}
