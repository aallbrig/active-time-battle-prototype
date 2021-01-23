using Finite_State_Machines;
using Finite_State_Machines.ActiveTimeBattle;
using UnityEngine;

namespace Controllers
{
    public class ActiveTimeBattleController : MonoBehaviour, IFiniteStateMachineContext<ActiveTimeBattleState>
    {
        public ActiveTimeBattleState CurrentState { get; set; }
        public void TransitionToState(ActiveTimeBattleState newState)
        {
            CurrentState.Leave(this);
            CurrentState = newState;
            CurrentState.Enter(this);
        }
    }
}
