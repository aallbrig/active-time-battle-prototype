using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Actions/RequeueActivePlayerAction")]
    public class RequeueActivePlayerAction : Action
    {
        public override void Act(FighterInputStateController controller)
        {
            controller.FighterReadyQueue.Enqueue(controller.input.fighter);
        }
    }
}