using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public class BattleState : ActiveTimeBattleState
    {
        private readonly PlayerBattleInputController _playerBattleInputController;

        public BattleState(ActiveTimeBattleController controller)
        {
            Controller = controller;
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

        public override IEnumerator Leave(Action callback)
        {
            _playerBattleInputController.gameObject.SetActive(false);
            return base.Leave(callback);
        }
    }
}