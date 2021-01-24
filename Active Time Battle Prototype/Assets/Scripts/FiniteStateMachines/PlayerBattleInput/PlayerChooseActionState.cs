using System;
using Controllers;
using UnityEngine;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerChooseActionState: PlayerBattleInputState
    {
        public PlayerChooseActionState(PlayerBattleInputController controller) : base(controller) {}

        public override void Enter()
        {
            // Determine list of player actions
            // Enable player actions UI
            // Show list of player actions in UI
            Controller.TogglePlayerActionsUi(true);
        }

        public override void Tick()
        {
            // Cycle through player input states
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.PlayerSelectTargetsState);
            }
        }

        public override void Leave(Action callback)
        {
            // Disable player actions UI
            base.Leave(callback);
        }
    }
}