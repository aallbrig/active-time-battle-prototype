﻿using Controllers;
using UnityEngine;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public class BattleLoseState : ActiveTimeBattleState
    {
        public BattleLoseState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.StartMenuState);
            }
        }
    }
}