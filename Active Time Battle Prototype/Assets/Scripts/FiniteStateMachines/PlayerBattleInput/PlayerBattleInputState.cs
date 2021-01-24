using System;
using Controllers;

namespace FiniteStateMachines.PlayerBattleInput
{
    public abstract class PlayerBattleInputState: IFiniteStateMachineState<PlayerBattleInputController>
    {
        public PlayerBattleInputController Controller { get; protected set; }
        
        protected PlayerBattleInputState(PlayerBattleInputController controller)
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