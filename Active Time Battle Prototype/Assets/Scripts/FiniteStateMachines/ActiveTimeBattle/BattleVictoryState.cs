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
            Controller.ToggleVictoryScreenUI(true);
        }

        public override void Leave(Action callback)
        {
            // Hide battle victory screen
            Controller.ToggleVictoryScreenUI(false);

            base.Leave(callback);
        }
    }
}