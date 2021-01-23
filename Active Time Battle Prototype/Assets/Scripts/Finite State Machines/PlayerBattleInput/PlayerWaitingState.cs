using System;
using Controllers;

namespace Finite_State_Machines.PlayerBattleInput
{
    public class PlayerWaitingState: PlayerBattleInputState
    {
        public PlayerWaitingState(PlayerBattleInputController controller)
        {
            Controller = controller;
        }
    }
}