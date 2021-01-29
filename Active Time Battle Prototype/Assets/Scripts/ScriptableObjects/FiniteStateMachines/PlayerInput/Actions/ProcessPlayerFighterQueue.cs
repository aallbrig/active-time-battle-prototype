using Commands;
using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Actions/ProcessPlayerFighterQueue")]
    public class ProcessPlayerFighterQueue : Action
    {
        public static event System.Action<ICommand> OnPlayerFighterCommand;
        public override void Act(PlayerInputStateController controller)
        {
            if (controller.playerFighterQueue.Count > 0)
            {
                var fighter = controller.playerFighterQueue.Dequeue();
                controller.SetActiveFighter(fighter);
            }
        }
    }
}