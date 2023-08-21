using UnityEngine;

namespace Project3D
{
    public class PlayerControllerRB : MyMonoBehaviour, IPlayerController
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CapsuleCollider capsuleCollider;

        public Vector3 Velocity;
        public Vector3 Acceleration;

        private Quaternion targetRotation = Quaternion.identity;
        private float rotationSpeed = 1200f; // turn 180 deg in 0.15s

        public Vector3 HorizontalVelocity => new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        public float HorizontalMoveSpeed => HorizontalVelocity.magnitude;
        public Vector3 HorizontalMoveDirection => HorizontalVelocity.normalized;

        public bool CanAirJump { get; set; } = true;
        public bool IsGrounded => true;
        public bool IsFalling => rigidbody.velocity.y < 0 && !IsGrounded;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Velocity += Acceleration * Time.deltaTime;
            rigidbody.velocity = Velocity;
        }

        private void Update()
        {
            
        }

        public void Move(Vector3 velocity)
        {

        }

        private void Rotate()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        public void SetUseGravity(bool isUse)
        {
            rigidbody.useGravity = isUse;
        }

        public void SetTargetRotation(Vector3 direction)
        {

        }

        void IPlayerController.Rotate()
        {

        }

        public void UseGravity(bool isUse)
        {

        }

        public void SnapToGround(float SnapSpeed)
        {

        }

        public bool OnSteepSlope(out RaycastHit downwardHit)
        {
            throw new System.NotImplementedException();
        }
    }
}
