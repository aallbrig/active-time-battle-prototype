using System;
using Controllers;
using Managers;

namespace FiniteStateMachines.PlayerBattleInput
{
    public abstract class PlayerBattleInputState: IFiniteStateMachineState<PlayerInputManager>
    {
        public PlayerInputManager Context { get; }
        
        protected PlayerBattleInputState(PlayerInputManager controller)
        {
            Context = controller;
        }

        public virtual void Enter() {}

        public virtual void Tick() {}

        public virtual void Leave(Action callback) => callback?.Invoke();
    }
}