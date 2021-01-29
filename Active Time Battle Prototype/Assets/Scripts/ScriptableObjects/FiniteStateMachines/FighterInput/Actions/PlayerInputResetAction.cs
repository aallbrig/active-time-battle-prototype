using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Actions/PlayerInputResetAction")]
    public class PlayerInputResetAction : Action
    {
        public override void Act(FighterInputStateController controller)
        {
            controller.ResetPlayerInput();
        }
    }
}