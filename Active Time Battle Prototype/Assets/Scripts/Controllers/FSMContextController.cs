using System;
using FiniteStateMachines;
using UnityEngine;

namespace Controllers
{
    public class FsmContextController<T, U> : MonoBehaviour, IFiniteStateMachineContext<T> where T : IFiniteStateMachineState<U>
    {
        public T CurrentState { get; private set; }

        public void TransitionToState(T newState)
        {
            var stateTransitionAction = new Action(() =>
            {
                CurrentState = (T) newState;
                CurrentState?.Enter();
            });

            if (CurrentState != null)
            {
                CurrentState.Leave(stateTransitionAction);
            }
            else
            {
                stateTransitionAction();
            }
        }

        private void Update() => CurrentState?.Tick();
    }
}