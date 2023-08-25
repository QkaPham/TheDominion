using UnityEngine;

namespace Project3D
{
    public class PlayerController : MyMonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private TargetSensor targetSensor;
        [field: SerializeField] public Weapon Weapon { get; private set; }
        [field: SerializeField] public WeaponPlacing WeaponPlacing { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }

        [SerializeField] private LayerMask groundLayer = 1 << 0 | 1 << 3;
        [SerializeField] public float slideLimit = 60;
        [SerializeField] float snapDistance = 0.15f;

        [SerializeField] public Vector3 velocity;
        [SerializeField] public Vector3 acceleration;

        private Quaternion targetRotation = Quaternion.identity;
        private float rotationSpeed = 1200f; // turn 180 deg in 0.15s

        public bool CanAirJump { get; set; } = true;
        public bool IsGrounded { get; private set; } = true;
        public bool IsFalling => velocity.y < 0 && !IsGrounded;

        public event System.Action DeathEvent;
        [SerializeField] private bool useRootMotion = false;
        private float slideTimer;
        public override void LoadComponent()
        {
            controller = GetComponent<CharacterController>();
            targetSensor = GetComponent<TargetSensor>();
            Weapon = GetComponent<Weapon>();
            Health = GetComponent<Health>();
            WeaponPlacing = GetComponent<WeaponPlacing>();
        }

        private void FixedUpdate()
        {
            GroundCheck();

            if (useRootMotion) return;
            velocity += acceleration * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        public void Move(Vector3 velocity)
        {
            this.velocity = velocity;
            SetTargetRotation(velocity);
        }

        public void MoveAndRotate(Vector3 velocity)
        {
            this.velocity.x = velocity.x;
            this.velocity.z = velocity.z;

            SetTargetRotation(velocity);
            Rotate();
        }

        public void SetVerticalVelocity(float velocityY) => velocity.y = velocityY;

        public void SetTargetRotation(Vector3 direction)
        {
            if (direction.x == 0 && direction.z == 0) return;
            direction.y = 0;
            targetRotation = Quaternion.LookRotation(direction);
        }

        private void Rotate() => transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        public void RotateToClosetEnemy()
        {
            var closetTarget = targetSensor.GetClosetTarget();
            if (closetTarget == null) return;

            var direction = closetTarget.transform.position - transform.position;
            var rotation = Quaternion.Euler(0f, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0f);
            transform.rotation = rotation;
        }

        public void UseGravity(bool isUse)
        {
            acceleration = isUse ? Physics.gravity : Vector3.zero;
        }

        public void GroundCheck()
        {
            Ray ray = new Ray(transform.position + controller.center, Vector3.down);
            IsGrounded = Physics.SphereCast(
                ray,
                controller.radius,
                controller.height / 2 - controller.radius + controller.skinWidth + Physics.defaultContactOffset,
                groundLayer);
        }

        public void SnapToGround(float SnapSpeed)
        {
            bool hasGroundBelow = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + snapDistance);
            velocity.y = hasGroundBelow ? SnapSpeed : Mathf.Max(velocity.y, 0f);
        }


        public bool OnSteepSlope(out RaycastHit downwardHit, float time = 0f)
        {
            if (Physics.SphereCast(transform.position + controller.center, controller.radius, Vector3.down, out downwardHit, Mathf.Infinity, groundLayer))
            {
                slideTimer += Time.deltaTime;
                if (Vector3.Angle(Vector3.up, downwardHit.normal) > slideLimit && slideTimer >= time)
                {
                    slideTimer = 0f;
                    return true;
                }
                return false;
            }
            slideTimer = 0f;
            return false;
        }

        public Vector3 ProjectAndScale(Vector3 velocity, Vector3 normal)
        {
            float speed = velocity.magnitude;
            Vector3 projected = Vector3.ProjectOnPlane(velocity, normal).normalized;
            return projected * speed;
        }

        public void Death()
        {
            DeathEvent?.Invoke();
        }

        public void ApplyRootMotion(bool apply)
        {
            GetComponentInChildren<RootMotionController>().Apply = apply;
            useRootMotion = apply;
        }
    }
}
