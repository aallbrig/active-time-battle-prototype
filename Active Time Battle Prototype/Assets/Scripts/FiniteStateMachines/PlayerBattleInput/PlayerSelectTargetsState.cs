using System;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerSelectTargetsState: PlayerBattleInputState
    {
        public PlayerSelectTargetsState(PlayerBattleInputController controller) : base(controller) {}

        public override void Enter()
        {
            // Find all valid player enemy targets
            // populate targets UI
            // show possible targets UI
            Controller.TogglePlayerTargetsUi(true);
        }

        public override void Tick()
        {
            // Cycle through player input states
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.PlayerWaitingState);
                // 25% change to exit this FSM
                // Controller.TransitionToState(Random.Range(0f, 1f) > 0.75f ? Controller.PlayerWaitingState : null);
            }
        }
    }
}