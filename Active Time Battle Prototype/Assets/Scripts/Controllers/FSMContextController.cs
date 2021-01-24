using Finite_State_Machines;
using UnityEngine;

namespace Controllers
{
    public class FSMContextController<T, U> : MonoBehaviour, IFiniteStateMachineContext<T> where T : IFiniteStateMachineState<U>
    {
        public T CurrentState { get; private set; }

        public void TransitionToState(T newState)
        {
            CurrentState?.Leave();
            CurrentState = (T) newState;
            CurrentState.Enter();
        }
    }
}