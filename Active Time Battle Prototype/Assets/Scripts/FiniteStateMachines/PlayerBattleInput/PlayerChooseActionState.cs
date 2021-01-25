using Controllers;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerChooseActionState: PlayerBattleInputState
    {
        public PlayerChooseActionState(PlayerBattleInputController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.TogglePlayerActionsUi(true);
        }
    }
}