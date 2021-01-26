using System;
using Controllers;
using Managers;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public abstract class ActiveTimeBattleState : IFiniteStateMachineState<ActiveTimeBattleManager>
    {
        public ActiveTimeBattleManager Context { get; }

        protected ActiveTimeBattleState(ActiveTimeBattleManager manager)
        {
            Context = manager;
        }

        public virtual void Enter() {}

        public virtual void Tick() {}

        public virtual void Leave(Action callback) => callback?.Invoke();
    }
}