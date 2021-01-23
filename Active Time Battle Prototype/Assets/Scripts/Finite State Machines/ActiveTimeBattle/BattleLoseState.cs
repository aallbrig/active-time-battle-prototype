using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public class BattleLoseState : ActiveTimeBattleState
    {
        public BattleLoseState(ActiveTimeBattleController controller)
        {
            Controller = controller;
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.StartMenuState);
            }
        }

        public override IEnumerator Leave(Action callback)
        {
            callback?.Invoke();
            yield break;
        }
    }
}