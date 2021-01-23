using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public class BattleVictoryState : ActiveTimeBattleState
    {
        public BattleVictoryState(ActiveTimeBattleController controller)
        {
            Controller = controller;
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BattleLoseState);
            }
        }

        public override IEnumerator Leave(Action callback)
        {
            callback?.Invoke();
            yield break;
        }
    }
}