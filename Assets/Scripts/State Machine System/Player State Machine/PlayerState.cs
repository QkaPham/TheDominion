using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public abstract class PlayerState : IState
    {
        protected abstract string StateName { get; set; }
        protected abstract float TransitionDuration { get; set; }
        protected virtual int StateHash { get; set; }
        protected Vector3 currentVelocity;
        protected float stateStartTime;

        protected Animator animator;
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        protected PlayerInput input;
        protected PlayerAnimationEvent animationEvent;

        public virtual bool HasRequestTransition() => false;
        public bool IsAnimationFinished => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(StateName) && !animator.IsInTransition(0);
        protected float StateDuration => Time.time - stateStartTime;

        public virtual void Initialize(Animator animator, PlayerController player, PlayerInput input, PlayerAnimationEvent animationEvent, PlayerStateMachine stateMachine)
        {
            this.animator = animator;
            this.player = player;
            this.input = input;
            this.stateMachine = stateMachine;
            this.animationEvent = animationEvent;
            StateHash = Animator.StringToHash(StateName);
        }

        public virtual void Enter()
        {
            animator.CrossFade(StateHash, TransitionDuration);
            stateStartTime = Time.time;
        }

        public virtual void Exit() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicUpdate() { }
    }
}
