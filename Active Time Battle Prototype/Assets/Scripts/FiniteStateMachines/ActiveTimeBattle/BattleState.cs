using System;
using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleState : ActiveTimeBattleState
    {
        private readonly PlayerBattleInputController _playerBattleInputController;

        public BattleState(ActiveTimeBattleController controller) : base(controller)
        {
            _playerBattleInputController = controller.playerBattleInputController;
        }

        public override void Enter()
        {
            _playerBattleInputController.gameObject.SetActive(true);
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BattleVictoryState);
            }
        }

        public override void Leave(Action callback)
        {
            _playerBattleInputController.gameObject.SetActive(false);
            base.Leave(callback);
        }
    }
}