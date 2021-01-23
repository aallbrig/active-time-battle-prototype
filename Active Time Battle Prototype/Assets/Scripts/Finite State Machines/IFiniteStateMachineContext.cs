namespace Finite_State_Machines
{
    public interface IFiniteStateMachineContext<T>
    {
        T CurrentState { get; }
        void TransitionToState(T newState);
    }
}