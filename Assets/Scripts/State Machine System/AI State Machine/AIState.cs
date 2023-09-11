using System;
using UnityEngine;
using UnityEngine.AI;

namespace Project3D
{
    [Serializable]
    public abstract class AIState : IState
    {
        protected abstract string StateName { get; set; }
        protected abstract float TransitionDuration { get; set; }
        protected virtual int StateHash { get; set; }

        protected AIController ai;
        protected Animator animator;
        protected AILook aiLook;
        protected AIStateMachine stateMachine;
        protected NavMeshAgent agent;
        protected TargetDetector targetDetector;

        protected float enterTime;
        protected float StateDuration => Time.time - enterTime;

        public void Initialize(Animator animator, AIController ai, NavMeshAgent agent, TargetDetector targetDetector, AILook aiLook, AIStateMachine stateMachine)
        {
            this.ai = ai;
            this.animator = animator;
            this.stateMachine = stateMachine;
            this.agent = agent;
            this.targetDetector = targetDetector;
            this.aiLook = aiLook;   
            StateHash = Animator.StringToHash(StateName);
        }

        public virtual bool HasTransitionRequest() => false;

        public virtual void Enter()
        {
            enterTime = Time.time;
            animator.CrossFade(StateHash, TransitionDuration);
        }

        public virtual void Exit() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicUpdate() { }
    }
}