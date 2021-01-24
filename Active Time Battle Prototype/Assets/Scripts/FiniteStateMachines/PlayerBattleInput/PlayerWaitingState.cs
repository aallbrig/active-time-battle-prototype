using Controllers;
using UnityEngine;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerWaitingState: PlayerBattleInputState
    {
        public PlayerWaitingState(PlayerBattleInputController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.TogglePlayerActionsUi(false);
            Controller.TogglePlayerTargetsUi(false);
        }

        public override void Tick()
        {
            // Cycle through player input states
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.PlayerChooseActionState);
            }
        }
    }
}