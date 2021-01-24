using System;

namespace FiniteStateMachines
{
    public interface IFiniteStateMachineState<out T>
    {
        T Controller { get; }
        
        void Enter();
        void Tick();
        void Leave(Action callback = null);
    }
}