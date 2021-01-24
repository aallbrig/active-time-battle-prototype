namespace FiniteStateMachines
{
    public interface IFiniteStateMachineContext<T>
    {
        T CurrentState { get; }
        void TransitionToState(T newState);
    }
}