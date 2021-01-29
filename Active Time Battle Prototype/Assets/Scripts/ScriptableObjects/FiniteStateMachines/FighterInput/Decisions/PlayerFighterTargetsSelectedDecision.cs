using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput.Decisions
{
    [CreateAssetMenu(menuName = "active time battle/FSM/player input/Decisions/PlayerFighterTargetsSelected")]
    public class PlayerFighterTargetsSelectedDecision : Decision
    {
        public override bool Decide(FighterInputStateController controller)
        {
            return controller.input.targets.Count > 0;
        }
    }
}