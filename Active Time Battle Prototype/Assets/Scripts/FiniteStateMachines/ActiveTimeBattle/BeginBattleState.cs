using System;
using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BeginBattleState : ActiveTimeBattleState
    {
        public BeginBattleState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            GeneratePlayerEnemies();
            // (optional) play "battle begin" camera animation
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BattleState);
            }
        }

        public override void Leave(Action callback)
        {
            base.Leave(callback);
        }

        private void GeneratePlayerEnemies()
        {
            // Generate player enemies, based on list of enemy prefabs
        }

    }
}