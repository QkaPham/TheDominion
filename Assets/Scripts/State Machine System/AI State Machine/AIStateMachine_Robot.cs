using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public class AIStateMachine_Robot : AIStateMachine
    {
        //[SerializeField] protected AIStateIdle idle;
        //[SerializeField] protected AIStateAttack attack;
        //[SerializeField] protected AIStateChase chase;
        //[SerializeField] protected AIStateStrafe strafe;
        //[SerializeField] protected AIStateStrafeBackward strafeBack;
        //[SerializeField] protected AIStateFindAttacker findAttacker;
        //[SerializeField] protected AIStateDefeat defeat;
        //[SerializeField] protected AIStateGiveUp giveUp;

        //private void Awake()
        //{
        //    idle.Initialize(animator, ai, agent, targetDetector, this);
        //    attack.Initialize(animator, ai, agent, targetDetector, this);
        //    chase.Initialize(animator, ai, agent, targetDetector, this);
        //    strafe.Initialize(animator, ai, agent, targetDetector, this);
        //    strafeBack.Initialize(animator, ai, agent, targetDetector, this);
        //    findAttacker.Initialize(animator, ai, agent, targetDetector, this);
        //    defeat.Initialize(animator, ai, agent, targetDetector, this);
        //    giveUp.Initialize(animator, ai, agent, targetDetector, this);

        //    AddTransition(idle, chase, () => targetDetector.HasTarget() && ai.CanAttack);
        //    AddTransition(idle, strafe, () => targetDetector.HasTarget() && !ai.CanAttack);

        //    AddTransition(attack, strafeBack, () => attack.IsAnimationFinished && targetDetector.HasTarget());
        //    AddTransition(attack, idle, () => attack.IsAnimationFinished && !targetDetector.HasTarget());

        //    AddTransition(chase, attack, chase.ReachAttackRange);
        //    AddTransition(chase, idle, () => !targetDetector.HasTarget());

        //    AddTransition(strafe, idle, () => !targetDetector.HasTarget());
        //    AddTransition(strafe, chase, () => ai.CanAttack);

        //    AddTransition(strafeBack, attack, () => strafeBack.ReachDesireDistance && ai.CanAttack);
        //    AddTransition(strafeBack, strafe, () => strafeBack.ReachDesireDistance && !ai.CanAttack);

        //    AddTransition(findAttacker, chase, () => targetDetector.HasTarget());
        //    AddTransition(findAttacker, idle, () => findAttacker.TimeOut);

        //    AddTransition(giveUp, idle, () => Vector3.Distance(transform.position, startPoint.position) < 0.1f);

        //    AddTransitionFromAny(giveUp, GiveUp);

        //    bool GiveUp()
        //    {
        //        if (!targetDetector.HasTarget()) return false;
        //        return Vector3.Distance(targetDetector.Target.position, startPoint.position) > activeRadius;
        //    }
        //}

        //private void Start()
        //{
        //    SwitchOn(idle);

        //    health.HealthChanged += TransitionOntakeDamage;
        //    health.Defeated += OnDefeat;
        //}


        //private void OnDestroy()
        //{
        //    health.HealthChanged -= TransitionOntakeDamage;
        //    health.Defeated -= OnDefeat;
        //}

        //private void TransitionOntakeDamage(float diff)
        //{
        //    if (!targetDetector.HasTarget() && diff < 0)
        //    {
        //        SwitchState(findAttacker);
        //    }
        //}

        //private void OnDefeat()
        //{
        //    SwitchState(defeat);
        //}
    }
}
