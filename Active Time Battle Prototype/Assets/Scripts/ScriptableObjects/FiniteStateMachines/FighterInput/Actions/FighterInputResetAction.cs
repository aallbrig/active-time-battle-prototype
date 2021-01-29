using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/fighter input/Actions/FighterInputResetAction")]
    public class FighterInputResetAction : Action
    {
        public override void Act(FighterInputStateController controller)
        {
            controller.input.ResetInput();
        }
    }
}