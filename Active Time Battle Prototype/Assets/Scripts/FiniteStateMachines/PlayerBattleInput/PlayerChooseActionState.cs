using Managers;
using UnityEngine;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerChooseActionState: PlayerBattleInputState
    {
        public PlayerChooseActionState(PlayerInputManager controller) : base(controller) {}

        public override void Enter()
        {
            Context.TogglePlayerActionsUi(true);
        }

        public override void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Context.TogglePlayerActionsUi(false);
                Context.ReEnqueueFighter(Context.activePlayerFighter);
                Context.TransitionToState(Context.PlayerWaitingState);
            }
        }
    }
}