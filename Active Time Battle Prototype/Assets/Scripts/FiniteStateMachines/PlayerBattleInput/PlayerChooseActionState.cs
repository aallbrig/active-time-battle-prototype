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
            Controller.TogglePlayerActionsUi(true);
        }

        public override void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Controller.TogglePlayerActionsUi(false);
                Controller.TransitionToState(Controller.PlayerWaitingState);
                var fighter = Controller.playerInput.ActiveFighter;
                Controller.ReEnqueueFighter(fighter);
            }
        }
    }
}