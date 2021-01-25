using System;
using System.Collections;
using ATBFighter;
using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleState : ActiveTimeBattleState
    {
        public static event Action<FighterController> OnBattleMeterTick;

        private readonly PlayerBattleInputController _playerBattleInputController;
        private IEnumerator _battleMeterTickCoroutine;
        private const float CoroutineExecutionWait = 0.75f;

        public BattleState(ActiveTimeBattleController controller) : base(controller)
        {
            _playerBattleInputController = controller.playerBattleInputController;
        }

        public override void Enter()
        {
            _playerBattleInputController.gameObject.SetActive(true);
            _playerBattleInputController.SetPlayerFighters(Controller.playerFighters);
            _playerBattleInputController.SetEnemyFighters(Controller.enemyFighters);

            // Show battle UI HUD
            Controller.ToggleBattleHUDUI(true);
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
            Controller.ToggleBattleHUDUI(false);
            Controller.ToggleBattleAnnouncements(false);

            base.Leave(callback);
        }

        private IEnumerator BattleMeterTickCoroutine()
        {
            while (true)
            {
                // For each ATB fighter, increment battle meter
                // If ATB fighter's battle meter is >= 1.0, trigger "ATB fighter ready to act" event (consumed by player, enemy AI)
                Controller.fighters.ForEach(fighter =>
                {
                    var battleMeterTickRate = 1 / fighter.stats.secondsToMaxBattleMeterValue;
                    var newBattleMeterValue = Mathf.Clamp(
                        fighter.stats.currentBattleMeterValue + battleMeterTickRate * CoroutineExecutionWait,
                        0f,
                        1f
                    );

                    fighter.stats.currentBattleMeterValue = newBattleMeterValue;

                    OnBattleMeterTick?.Invoke(fighter);
                });
                
                yield return new WaitForSeconds(CoroutineExecutionWait);
            }
        }
    }
}