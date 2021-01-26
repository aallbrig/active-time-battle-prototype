using Controllers;
using Managers;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerWaitingState: PlayerBattleInputState
    {
        public PlayerWaitingState(PlayerInputManager controller) : base(controller) {}

        public override void Enter()
        {
            Context.TogglePlayerActionsUi(false);
            Context.TogglePlayerTargetsUi(false);
        }
    }
}