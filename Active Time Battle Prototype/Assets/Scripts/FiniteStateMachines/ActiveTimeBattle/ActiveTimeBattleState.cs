using System;
using Controllers;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public abstract class ActiveTimeBattleState : IFiniteStateMachineState<ActiveTimeBattleController>
    {
        public ActiveTimeBattleController Controller { get; }

        protected ActiveTimeBattleState(ActiveTimeBattleController controller)
        {
            Controller = controller;
        }

        public virtual void Enter() {}

        public virtual void Tick() {}

        public virtual void Leave(Action callback) => callback?.Invoke();
    }
}