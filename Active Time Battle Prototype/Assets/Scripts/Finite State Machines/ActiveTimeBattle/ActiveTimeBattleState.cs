﻿using System;
using System.Collections;
using Controllers;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public abstract class ActiveTimeBattleState : IFiniteStateMachineState<ActiveTimeBattleController>
    {
        public ActiveTimeBattleController Controller { get; protected set; }

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