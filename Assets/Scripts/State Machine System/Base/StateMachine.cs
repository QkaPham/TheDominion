using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project3D
{
    public enum TransitionEventId
    {
        LoseHealth,
        Defeat
    }

    public class StateMachine : MyMonoBehaviour
    {
        protected IState currentState;

        protected Dictionary<IState, List<Transition>> transitions = new();
        private List<Transition> transitionsFromAny = new();

        void FixedUpdate()
        {
            currentState.PhysicUpdate();
        }

        protected virtual void Update()
        {
            CheckTransition();
            currentState.LogicUpdate();
        }

        protected void SwitchOn(IState newState)
        {
            currentState = newState;
            currentState.Enter();
        }

        public void SwitchState(IState newState)
        {
            currentState.Exit();
            SwitchOn(newState);
        }


        protected void AddTransition(IState fromState, IState toState, Func<bool> condition)
        {
            if (!transitions.ContainsKey(fromState))
            {
                transitions.Add(fromState, new List<Transition>());
            }
            transitions[fromState].Add(new Transition(toState, condition));
        }

        protected void AddTransitionFromAny(IState toState, Func<bool> condition)
        {
            transitionsFromAny.Add(new Transition(toState, condition));
        }

        protected void CheckTransition()
        {
            foreach (var transition in transitionsFromAny)
            {
                if (transition.Condition())
                {
                    SwitchState(transition.ToState);
                }
            }

            foreach (var transition in transitions.GetValueOrDefault(currentState))
            {
                if (transition.Condition())
                {
                    SwitchState(transition.ToState);
                }
            }
        }

        protected class Transition
        {
            public IState ToState { get; }
            public Func<bool> Condition { get; }

            public Transition(IState toState, Func<bool> condition)
            {
                ToState = toState;
                Condition = condition;
            }
        }
    }
}
