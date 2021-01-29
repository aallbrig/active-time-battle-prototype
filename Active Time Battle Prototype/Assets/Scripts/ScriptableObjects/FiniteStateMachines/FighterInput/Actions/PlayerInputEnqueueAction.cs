using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Actions/PlayerInputEnqueueAction")]
    public class PlayerInputEnqueueAction : Action
    {
        public override void Act(FighterInputStateController controller)
        {
            controller.SubmitFighterInput(
                controller.input.fighter,
                controller.input.action,
                controller.input.targets
            );
        }
    }
}