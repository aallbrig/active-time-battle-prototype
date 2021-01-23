using System;
using Controllers;

namespace Finite_State_Machines.PlayerBattleInput
{
    public class PlayerSelectTargetsState: PlayerBattleInputState
    {
        public PlayerSelectTargetsState(PlayerBattleInputController controller)
        {
            Controller = controller;
        }
    }
}