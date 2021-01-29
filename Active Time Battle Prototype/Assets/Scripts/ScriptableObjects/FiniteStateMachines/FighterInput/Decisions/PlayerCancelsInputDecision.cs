using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Decisions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Decisions/PlayerCancelsInput")]
    public class PlayerCancelsInputDecision : Decision
    {
        public KeyCode cancelKey;
        public override bool Decide(FighterInputStateController controller)
        {
            return Input.GetKeyDown(cancelKey);
        }
    }
}