using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput.Decisions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/decisions/PlayerCancelsInput")]
    public class PlayerCancelsInputDecision : Decision
    {
        public KeyCode cancelKey;
        public override bool Decide(PlayerInputStateController controller)
        {
            return Input.GetKeyDown(cancelKey);
        }
    }
}