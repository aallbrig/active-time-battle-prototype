using System;
using Controllers;

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
        }

        public override void Leave(Action callback)
        {
            // Disable player actions UI
            base.Leave(callback);
        }
    }
}