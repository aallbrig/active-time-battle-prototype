using Commands;
using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput.Actions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Actions/PlayerInputEnqueueAction")]
    public class PlayerInputEnqueueAction : Action
    {
        public static event System.Action<ICommand> OnPlayerFighterCommand;
        public override void Act(PlayerInputStateController controller)
        {
            OnPlayerFighterCommand?.Invoke(new BattleCommand(
                controller.activePlayerFighter,
                controller.activePlayerFighterAction,
                controller.activePlayerFighterTargets
            ));
        }
    }
}