using System;
using Controllers;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public abstract class ActiveTimeBattleState : IFiniteStateMachineState<ActiveTimeBattleController>
    {
        public ActiveTimeBattleController Controller { get; protected set; }

        protected ActiveTimeBattleState(ActiveTimeBattleController controller)
        {
            Controller = controller;
        }

        public virtual void Enter()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Tick()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Leave(Action callback)
        {
            callback?.Invoke();
        }
    }
}