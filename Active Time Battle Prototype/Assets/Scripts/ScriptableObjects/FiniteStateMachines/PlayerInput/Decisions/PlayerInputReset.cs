using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput.Decisions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/decisions/PlayerInputReset")]
    public class PlayerInputReset : Decision
    {
        public override bool Decide(PlayerInputStateController controller)
        {
            return controller.activePlayerFighter == null
               && controller.activePlayerFighterAction == null
               && controller.activePlayerFighterTargets.Count == 0;
        }
    }
}