using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Actions/ProcessPlayerFighterQueue")]
    public class ProcessPlayerFighterQueue : Action
    {
        public override void Act(FighterInputStateController controller)
        {
            if (controller.FighterReadyQueue.Count > 0)
            {
                var fighter = controller.FighterReadyQueue.Dequeue();
                controller.SetActiveFighter(fighter);
            }
        }
    }
}