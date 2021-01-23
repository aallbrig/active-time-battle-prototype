using System;
using System.Collections;

namespace Finite_State_Machines
{
    public interface IFiniteStateMachineState<out T>
    {
        T Controller { get; }
        
        void Enter();
        void Tick();
        IEnumerator Leave(Action callback);
    }
}