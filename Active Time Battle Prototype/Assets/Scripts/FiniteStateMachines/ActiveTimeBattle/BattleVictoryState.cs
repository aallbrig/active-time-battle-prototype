using System;
using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleVictoryState : ActiveTimeBattleState
    {
        public BattleVictoryState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            // Show battle victory screen
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BattleLoseState);
            }
        }

        public override void Leave(Action callback)
        {
            // Hide battle victory screen

            base.Leave(callback);
        }
    }
}