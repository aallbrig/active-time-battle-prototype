using System;
using System.Collections;
using System.Linq;
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
        private const float CoroutineExecutionWait = 0.1f;

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
            var enemiesCurrentHealth =
                Controller.enemyFighters.Aggregate(0f, (sum, fighter) => sum + fighter.stats.currentHealth);
            var playersCurrentHealth =
                Controller.playerFighters.Aggregate(0f, (sum, fighter) => sum + fighter.stats.currentHealth);

            if (enemiesCurrentHealth <= 0) Controller.TransitionToState(Controller.BattleVictoryState);
            else if (playersCurrentHealth <= 0) Controller.TransitionToState(Controller.BattleLoseState);
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
                // If ATB fighter's battle meter is >= 1.0, trigger "ATB fighter ready to act" event (consumed by player, enemy AI)
                Controller.fighters.ForEach(fighter =>
                {
                    var battleMeterTickRate = 1 / fighter.stats.secondsToMaxBattleMeterValue;
                    var newBattleMeterValue = Mathf.Clamp(
                        fighter.stats.currentBattleMeterValue + battleMeterTickRate * CoroutineExecutionWait,
                        0f,
                        1f
                    );

                    // If the fighter's battle meter is already full, no need to send more on battle meter tick events
                    if (fighter.stats.currentBattleMeterValue != 1.0f)
                    {
                        fighter.stats.currentBattleMeterValue = newBattleMeterValue;
                        OnBattleMeterTick?.Invoke(fighter);
                    }
                });
                
                yield return new WaitForSeconds(CoroutineExecutionWait);
            }
        }
    }
}