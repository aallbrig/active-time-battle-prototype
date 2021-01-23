﻿using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public class StartMenuState : ActiveTimeBattleState
    {
        public StartMenuState(ActiveTimeBattleController controller)
        {
            Controller = controller;
        }

        public override void Enter()
        {
            // Play start menu on enter animation
            // User is presented with start menu title
            // User is presented with start fight button
            // User is presented with quit game button
        }
        public override void Tick()
        {
            // On start fight button
            // On quit game button

            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BeginBattleState);
            }
        }

        public override IEnumerator Leave(Action callback)
        {
            callback?.Invoke();
            yield break;
        }
    }
}