using Commands;
using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Actions/PlayerInputResetAction")]
    public class PlayerInputResetAction : Action
    {
        public override void Act(PlayerInputStateController controller)
        {
            controller.ResetPlayerInput();
        }
    }
}