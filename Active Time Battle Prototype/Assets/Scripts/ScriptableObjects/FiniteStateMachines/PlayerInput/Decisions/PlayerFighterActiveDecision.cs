using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/decisions/PlayerFighterActive")]
    public class PlayerFighterActiveDecision : Decision
    {
        public override bool Decide(PlayerInputStateController controller)
        {
            return controller.activePlayerFighter != null;
        }
    }
}