using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleState : ActiveTimeBattleState
    {
        private readonly PlayerBattleInputController _playerBattleInputController;
        private IEnumerator _battleMeterTickCoroutine;

        public BattleState(ActiveTimeBattleController controller) : base(controller)
        {
            _playerBattleInputController = controller.playerBattleInputController;
        }

        public override void Enter()
        {
            _playerBattleInputController.gameObject.SetActive(true);
            // Show battle UI HUD
            // (optional) animate/set camera to battle state position
            _battleMeterTickCoroutine = BattleMeterTickCoroutine();
            Controller.StartCoroutine(_battleMeterTickCoroutine);
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
            Controller.StopCoroutine(_battleMeterTickCoroutine);
            _playerBattleInputController.gameObject.SetActive(false);

            // Hide battle UI HUD

            base.Leave(callback);
        }

        private IEnumerator BattleMeterTickCoroutine()
        {
            // For each ATB fighter, increment battle meter
            // If ATB fighter's battle meter is >= 1.0, trigger "ATB fighter ready to act" event (consumed by player, enemy AI)
            yield return null;
        }
    }
}