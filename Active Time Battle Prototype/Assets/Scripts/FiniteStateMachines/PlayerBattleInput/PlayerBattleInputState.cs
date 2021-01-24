using System;
using Controllers;

namespace FiniteStateMachines.PlayerBattleInput
{
    public abstract class PlayerBattleInputState: IFiniteStateMachineState<PlayerBattleInputController>
    {
        public PlayerBattleInputController Controller { get; }
        
        protected PlayerBattleInputState(PlayerBattleInputController controller)
        {
            Controller = controller;
        }

        public virtual void Enter() {}

        public virtual void Tick() {}

        public virtual void Leave(Action callback) => callback?.Invoke();
    }
}