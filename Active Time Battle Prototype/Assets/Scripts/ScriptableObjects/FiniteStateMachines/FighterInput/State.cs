using System.Collections.Generic;
using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput
{
    [CreateAssetMenu(fileName = "new state", menuName = "active time battle/FSM/player input/state", order = 0)]
    public class State : ScriptableObject
    {
        public List<Action> actions;
        public List<Transition> transitions;

        public void UpdateState(FighterInputStateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(FighterInputStateController controller)
        {
            actions.ForEach(action => action.Act(controller));
        }

        private void CheckTransitions(FighterInputStateController controller)
        {
            transitions.ForEach(transition =>
            {
                if (transition.decision.Decide(controller))
                    controller.TransitionToState(transition.trueState);
            });
        }
    }
}