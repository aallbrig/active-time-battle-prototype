using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Decisions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Decisions/PlayerFighterActive")]
    public class PlayerFighterActiveDecision : Decision
    {
        public override bool Decide(FighterInputStateController controller)
        {
            return controller.input.fighter != null;
        }
    }
}