namespace Finite_State_Machines
{
    public interface IFiniteStateMachineContext<T>
    {
        T CurrentState { get; set; }
        void TransitionToState(T newState);
    }
}