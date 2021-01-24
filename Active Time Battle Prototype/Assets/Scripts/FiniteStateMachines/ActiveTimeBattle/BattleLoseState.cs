using System;
using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleLoseState : ActiveTimeBattleState
    {
        public BattleLoseState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.ToggleLoseScreenUI(true);
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.StartMenuState);
            }
        }

        public override void Leave(Action callback)
        {
            Controller.ToggleLoseScreenUI(false);
            base.Leave(callback);
        }
    }
}