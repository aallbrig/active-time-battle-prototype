using System;
using System.Collections;
using Controllers;

namespace Finite_State_Machines.PlayerBattleInput
{
    public abstract class PlayerBattleInputState: IFiniteStateMachineState<PlayerBattleInputController>
    {
        public PlayerBattleInputController Controller { get; protected set; }
        public virtual void Enter()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Tick()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerator Leave(Action callback)
        {
            callback?.Invoke();
            yield break;
        }
    }
}