using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class StartMenuState : ActiveTimeBattleState
    {
        public StartMenuState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            // Play start menu on enter animation
            // User is presented with start menu title
            // User is presented with start fight button
            // User is presented with quit game button

            // On start fight button
            // On quit game button
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BeginBattleState);
            }
        }
    }
}