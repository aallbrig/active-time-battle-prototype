using Controllers;
using Managers;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerActionWaitingState: PlayerBattleInputState
    {
        public PlayerActionWaitingState(PlayerInputManager controller) : base(controller) {}
    }
}