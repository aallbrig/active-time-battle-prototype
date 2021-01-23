using System;
using System.Collections;
using Controllers;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public abstract class ActiveTimeBattleState : IFiniteStateMachineState<ActiveTimeBattleController>
    {
        public ActiveTimeBattleController Controller { get; set; }

        public abstract void Enter();
        public abstract void Tick();
        public abstract IEnumerator Leave(Action callback);
    }
}