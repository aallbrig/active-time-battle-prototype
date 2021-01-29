using System.Collections.Generic;
using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput
{
    [CreateAssetMenu(fileName = "new state", menuName = "active time battle/FSM/player input/state", order = 0)]
    public class State : ScriptableObject
    {
        public List<Action> actions;
        public List<Transition> transitions;

        public void UpdateState(PlayerInputStateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(PlayerInputStateController controller)
        {
            actions.ForEach(action => action.Act(controller));
        }

        private void CheckTransitions(PlayerInputStateController controller)
        {
            transitions.ForEach(transition =>
            {
                if (transition.decision.Decide(controller))
                    controller.TransitionToState(transition.trueState);
            });
        }
    }
}