using System;
using UnityEngine;
using UnityEngine.AI;

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

        public override void Enter()
        {
            base.Enter();

            RotateToTarget();
            stateMachine.AiSkill.Activate(agent);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (agent.enabled)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            agent.enabled = false;
                            agent.velocity = Vector3.zero;
                        }
                    }
                }
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

        private void RotateToTarget()
        {
            var direction = targetDetector.TargetDirection;
            direction.y = 0;
            stateMachine.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}