using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Project3D
{
    public class PlayerInput : MyMonoBehaviour
    {
        private CinemachineInputProvider freeLookCameraInput;

        [SerializeField] private ControlsID startControls = ControlsID.UI;
        public Controls Controls { get; private set; }
        private EventSystem eventSystem;
        private float CamreraRotation => Camera.main.transform.rotation.eulerAngles.y;
        public Vector2 MoveAxes => Quaternion.Euler(0, 0, -CamreraRotation) * Controls.Player.Move.ReadValue<Vector2>();
        public Vector3 MoveAxesXZ
        {
            get
            {
                var axes = MoveAxes;
                return new Vector3(axes.x, 0f, axes.y);
            }
        }
        public bool Move => MoveAxes != Vector2.zero;

        public bool Jump => Controls.Player.Jump.WasPressedThisFrame();
        public bool StopJump => Controls.Player.Jump.WasReleasedThisFrame();


        [SerializeField] private float jumpBufferTime = 0.5f;
        private float lastJumpInputTime;
        private bool hasJumpBuffer;
        public bool HasJumpBuffer
        {
            get => hasJumpBuffer ? Time.time - lastJumpInputTime <= jumpBufferTime : hasJumpBuffer;
            set
            {
                hasJumpBuffer = value;
                lastJumpInputTime = Time.time;
            }
        }


        public bool Attack => Controls.Player.Attack.WasPressedThisFrame();

        [SerializeField] private float attackBufferTime;
        private float lastAttackInputTime;

        private bool hasAttackBuffer;
        public bool HasAttackBuffer
        {
            get => hasAttackBuffer ? Time.time - lastAttackInputTime <= attackBufferTime : hasAttackBuffer;
            set
            {
                hasAttackBuffer = value;
                lastAttackInputTime = Time.time;
            }
        }

        public bool Interact => Controls.Player.Interact.WasPressedThisFrame();
        public bool Pause => Controls.Player.Pause.WasPressedThisFrame();
        public bool Dash => Controls.Player.Dash.WasPressedThisFrame();
        public bool UsePotion => Controls.Player.UsePotion.WasPressedThisFrame();

        public bool Command => Controls.Player.Command.WasPressedThisFrame();

        public bool Cancel => Controls.UI.Cancel.WasPressedThisFrame();

        private void Awake()
        {
            Controls = new Controls();
            eventSystem = EventSystem.current;
            freeLookCameraInput = FindObjectOfType<CinemachineInputProvider>();
        }

        private void Start()
        {
            SwitchControls(startControls);
        }

        private void OnEnable()
        {
            Controls.Player.Jump.canceled += ctx => HasJumpBuffer = false;
        }

        public void SwitchControls(ControlsID id)
        {
            switch (id)
            {
                case ControlsID.Player:
                    EnablePlayerInput(true);
                    EnableUIInput(false);
                    break;
                case ControlsID.UI:
                    EnablePlayerInput(false);
                    EnableUIInput(true);
                    break;
            };
        }

        public void EnablePlayerInput(bool enable)
        {
            if (enable) Controls.Player.Enable();
            else Controls.Player.Disable();
            freeLookCameraInput.enabled = enable;
        }

        public void EnableUIInput(bool enable)
        {
            if (enable) Controls.UI.Enable();
            else Controls.UI.Disable();
            eventSystem.enabled = enable;
            Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
