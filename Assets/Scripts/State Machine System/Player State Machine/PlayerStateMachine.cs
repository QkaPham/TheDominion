using UnityEngine;
using System.Collections.Generic;
using System;

namespace Project3D
{
    public class PlayerStateMachine : StateMachine
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerController player;
        [SerializeField] private Health health;
        [SerializeField] private PlayerAnimationEvent animationEvent;

        protected bool IsAnimationFinished => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f;

        public override void LoadComponent()
        {
            animator = GetComponentInChildren<Animator>();
            input = GetComponent<PlayerInput>();
            player = GetComponent<PlayerController>();
            health = GetComponent<Health>();
            animationEvent = GetComponentInChildren<PlayerAnimationEvent>();
        }

        [Header("States")]
        [SerializeField] private PlayerStateIdle idle = new();
        [SerializeField] private PlayerStateRun run = new();
        [SerializeField] private PlayerStateJump jump = new();
        [SerializeField] private PlayerStateFall fall = new();
        [SerializeField] private PlayerStateSlide slide = new();
        [SerializeField] private PlayerStateDash dash = new();
        [SerializeField] private PlayerStateAirJump airJump = new();
        [SerializeField] private PlayerStateLand land = new();
        [SerializeField] private PlayerStateAttack attack = new();
        [SerializeField] private PlayerStateDefeat defeat = new();

        private void Awake()
        {
            idle.Initialize(animator, player, input, animationEvent, this);
            run.Initialize(animator, player, input, animationEvent, this);
            jump.Initialize(animator, player, input, animationEvent, this);
            fall.Initialize(animator, player, input, animationEvent, this);
            slide.Initialize(animator, player, input, animationEvent, this);
            attack.Initialize(animator, player, input, animationEvent, this);
            dash.Initialize(animator, player, input, animationEvent, this);
            airJump.Initialize(animator, player, input, animationEvent, this);
            land.Initialize(animator, player, input, animationEvent, this);
            defeat.Initialize(animator, player, input, animationEvent, this);

            AddTransition(idle, run, () => input.Move);
            AddTransition(idle, jump, () => input.Jump);
            AddTransition(idle, fall, () => !player.IsGrounded);
            AddTransition(idle, slide, () => idle.IsOnSteepSlope);
            AddTransition(idle, attack, () => input.Attack);
            AddTransition(idle, dash, () => input.Dash);

            AddTransition(run, idle, () => !input.Move);
            AddTransition(run, jump, () => input.Jump);
            AddTransition(run, fall, () => !player.IsGrounded);
            AddTransition(run, slide, () => run.IsOnSteepSlope);
            AddTransition(run, attack, () => input.Attack);
            AddTransition(run, dash, () => input.Dash);

            AddTransition(jump, fall, () => player.IsFalling);
            AddTransition(jump, land, () => jump.IsUngrounded && player.IsGrounded);
            AddTransition(jump, airJump, () => input.Jump && player.CanAirJump);

            AddTransition(fall, land, () => player.IsGrounded);
            AddTransition(fall, airJump, () => input.Jump && player.CanAirJump);

            AddTransition(slide, idle, () => !slide.IsOnSteepSlope);

            AddTransition(attack, idle, () => IsAnimationFinished);
            AddTransition(attack, run, () => attack.IsAttackFinished && !(input.Attack || input.HasAttackBuffer) && input.Move);
            AddTransition(attack, dash, () => attack.IsAttackFinished && !(input.Attack || input.HasAttackBuffer) && input.Dash);

            AddTransition(dash, idle, () => dash.IsDashFinished);

            AddTransition(airJump, fall, () => player.IsFalling);
            AddTransition(airJump, land, () => player.IsGrounded);

            AddTransition(land, run, () => land.IsLandFinished && input.Move);
            AddTransition(land, idle, () => IsAnimationFinished);
            AddTransition(land, slide, () => player.OnSteepSlope(out _));
            AddTransition(land, jump, () => input.HasJumpBuffer || input.Jump);

            AddTransitionFromAny(defeat, () => currentState != defeat && health.IsDead);
        }

        private void Start()
        {
            SwitchOn(idle);
        }
    }
}
