using System;
using Controllers;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerSelectTargetsState: PlayerBattleInputState
    {
        public PlayerSelectTargetsState(PlayerBattleInputController controller) : base(controller) {}

        public override void Enter()
        {
            // Find all valid player enemy targets
            // populate targets UI
            // show possible targets UI
        }
    }
}