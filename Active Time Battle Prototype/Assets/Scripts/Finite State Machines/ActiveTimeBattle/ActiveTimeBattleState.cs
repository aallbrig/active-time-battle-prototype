using System.Collections;
using Controllers;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public abstract class ActiveTimeBattleState : IFiniteStateMachineState<ActiveTimeBattleController>
    {
        public abstract IEnumerator Enter(ActiveTimeBattleController controller);
        public abstract IEnumerator Tick(ActiveTimeBattleController controller);
        public abstract IEnumerator Leave(ActiveTimeBattleController controller);
    }
}