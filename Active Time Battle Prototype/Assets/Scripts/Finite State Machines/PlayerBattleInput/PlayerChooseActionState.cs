using Controllers;

namespace Finite_State_Machines.PlayerBattleInput
{
    public class PlayerChooseActionState: PlayerBattleInputState
    {
        public PlayerChooseActionState(PlayerBattleInputController controller)
        {
            Controller = controller;
        }
    }
}