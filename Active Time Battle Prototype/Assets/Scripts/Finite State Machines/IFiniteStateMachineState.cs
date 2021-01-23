using System.Collections;

namespace Finite_State_Machines
{
    public interface IFiniteStateMachineState<in T>
    {
        IEnumerator Enter(T controller);
        IEnumerator Tick(T controller);
        IEnumerator Leave(T controller);
    }
}