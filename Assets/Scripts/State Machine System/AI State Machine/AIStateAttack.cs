using System;
using UnityEngine;

namespace Project3D
{
    [Serializable]
    public class AIStateAttack : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Attack";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0f;

        [SerializeField] private float startChasingDistance = 2f;
        public override bool HasTransitionRequest() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(stateMachine.AiSkill.ReadySkill.Name);
        protected override int StateHash => stateMachine.AiSkill.ReadySkill.Hash;

        protected bool IsAgentReachDestination()
        {
            if (agent.enabled)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.AiSkill.Activate(stateMachine);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsAgentReachDestination())
            {
                agent.enabled = false;
                agent.velocity = Vector3.zero;
            }
        }

        public override void Exit()
        {
            base.Exit();

            agent.stoppingDistance = 0f;
            agent.enabled = true;
            aiLook.LockOn();
            stateMachine.AiSkill.CoolDown();
        }
    }

    [Serializable]
    public class AIStateAttackRootMotion : AIState
    {
        [field: SerializeField] protected override string StateName { get; set; } = "Attack";
        [field: SerializeField, Range(0f, 1f)] protected override float TransitionDuration { get; set; } = 0f;

        public override bool HasTransitionRequest() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && animator.GetCurrentAnimatorStateInfo(0).IsName(stateMachine.AiSkill.ReadySkill.Name);
        protected override int StateHash => stateMachine.AiSkill.ReadySkill.Hash;

        [SerializeField] protected float moveSpeed = 1f;
        [SerializeField] protected float rotationSpeed = 1f;

        public override void Enter()
        {
            base.Enter();

            rootMotionAgent.Apply(true, moveSpeed, rotationSpeed);
            stateMachine.AiSkill.Activate(stateMachine);
        }

        public override void Exit()
        {
            base.Exit();

            rootMotionAgent.Apply(false);
            aiLook.LockOn();
            stateMachine.AiSkill.CoolDown();
        }
    }
}