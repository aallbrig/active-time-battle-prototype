using Controllers;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerWaitingState: PlayerBattleInputState
    {
        public PlayerWaitingState(PlayerBattleInputController controller) : base(controller) {}

        public override void Enter()
        {
            // Hide player actions UI
            // Hide player target UI
        }
    }
}