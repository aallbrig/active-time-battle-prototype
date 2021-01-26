using System;

namespace FiniteStateMachines
{
    public interface IFiniteStateMachineState<out T>
    {
        T Context { get; }
        
        void Enter();
        void Tick();
        void Leave(Action callback = null);
    }
}