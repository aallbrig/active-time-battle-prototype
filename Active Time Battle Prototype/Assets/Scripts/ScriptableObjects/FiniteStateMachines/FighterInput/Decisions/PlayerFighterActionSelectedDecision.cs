using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Decisions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Decisions/PlayerFighterActionSelected")]
    public class PlayerFighterActionSelectedDecision : Decision
    {
        public override bool Decide(FighterInputStateController controller)
        {
            return controller.input.action != null;
        }
    }
}