using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/decisions/PlayerFighterTargetsSelected")]
    public class PlayerFighterTargetsSelectedDecision : Decision
    {
        public override bool Decide(PlayerInputStateController controller)
        {
            return controller.activePlayerFighterTargets.Count > 0;
        }
    }
}