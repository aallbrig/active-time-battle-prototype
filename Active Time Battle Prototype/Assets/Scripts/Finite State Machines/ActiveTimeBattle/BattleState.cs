﻿using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public class BattleState : ActiveTimeBattleState
    {
        public BattleState(ActiveTimeBattleController controller)
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
                Controller.TransitionToState(Controller.BattleVictoryState);
            }
        }

        public override IEnumerator Leave(Action callback)
        {
            callback?.Invoke();
            yield break;
        }
    }
}