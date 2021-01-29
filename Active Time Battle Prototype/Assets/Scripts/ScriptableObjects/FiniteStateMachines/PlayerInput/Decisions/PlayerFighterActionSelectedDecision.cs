using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput.Decisions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/decisions/PlayerFighterActionSelected")]
    public class PlayerFighterActionSelectedDecision : Decision
    {
        public override bool Decide(PlayerInputStateController controller)
        {
            return controller.activePlayerFighterAction != null;
        }
    }
}