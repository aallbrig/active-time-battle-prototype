using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Actions/PlayerInputEnqueueAction")]
    public class PlayerInputEnqueueAction : Action
    {
        public override void Act(PlayerInputStateController controller)
        {
            controller.SubmitPlayerInput(
                controller.activePlayerFighter,
                controller.activePlayerFighterAction,
                controller.activePlayerFighterTargets
            );
        }
    }
}