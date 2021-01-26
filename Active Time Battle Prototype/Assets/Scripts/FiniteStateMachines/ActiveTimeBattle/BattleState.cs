using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleState : ActiveTimeBattleState
    {
        public static event Action<FighterController> OnBattleMeterTick;

        private readonly PlayerBattleInputController _playerBattleInputController;
        private IEnumerator _battleMeterTickCoroutine;
        private const float CoroutineWaitInSeconds = 0.1f;
        private bool _checkBattleConclusionCondition;
        private List<FighterController> _fighters = new List<FighterController>();

        public BattleState(ActiveTimeBattleController controller) : base(controller)
        {
            _playerBattleInputController = controller.playerBattleInputController;
        }

        public override void Enter()
        {
            _fighters = _fighters.Concat(Controller.PlayerFighters).ToList();
            _fighters = _fighters.Concat(Controller.EnemyFighters).ToList();

            _playerBattleInputController.gameObject.SetActive(true);

            // Show battle UI HUD
            Controller.ToggleBattleHUDUI(true);
            // (optional) animate/set camera to battle state position

            _battleMeterTickCoroutine = BattleMeterTickCoroutine();
            Controller.StartCoroutine(_battleMeterTickCoroutine);

            _checkBattleConclusionCondition = true;
        }

        public override void Tick()
        {
            if (!_checkBattleConclusionCondition) return;
            CheckForBattleConclusionCondition();
        }

        public override void Leave(Action callback)
        {
            Controller.StartCoroutine(OnLeaveCoroutine(callback));
        }

        private IEnumerator OnLeaveCoroutine(Action callback)
        {
            Controller.StopCoroutine(_battleMeterTickCoroutine);
            _fighters = new List<FighterController>();
            _playerBattleInputController.gameObject.SetActive(false);

            // Hide battle UI HUD
            Controller.ToggleBattleHUDUI(false);
            Controller.ToggleBattleAnnouncements(false);

            yield return new WaitForSeconds(5);

            base.Leave(callback);
        }


        private float SumHealth(List<FighterController> fighters) =>
            fighters.Aggregate(0f, (sum, fighter) => sum + fighter.stats.currentHealth);

        private void CheckForBattleConclusionCondition()
        {
            var enemiesCurrentHealth = SumHealth(Controller.EnemyFighters);
            var playersCurrentHealth = SumHealth(Controller.PlayerFighters);

            if (enemiesCurrentHealth <= 0)
            {
                _checkBattleConclusionCondition = false;
                Controller.TransitionToState(Controller.BattleVictoryState);
            }
            else if (playersCurrentHealth <= 0)
            {
                _checkBattleConclusionCondition = false;
                Controller.TransitionToState(Controller.BattleLoseState);
            }
        }

        private void TickBattleMeterForFighter(FighterController fighter, float secondsSinceLastTick)
        {
            var battleMeterTickRateInSeconds = 1 / fighter.stats.secondsToMaxBattleMeterValue;
            var newBattleMeterValue = Mathf.Clamp(
                fighter.stats.currentBattleMeterValue + (battleMeterTickRateInSeconds * secondsSinceLastTick),
                0f,
                1f
            );

            fighter.stats.currentBattleMeterValue = newBattleMeterValue;
            OnBattleMeterTick?.Invoke(fighter);
    }

        private IEnumerator BattleMeterTickCoroutine()
        {
            while (true)
            {
                // If ATB fighter's battle meter is >= 1.0, trigger "ATB fighter ready to act" event (consumed by player, enemy AI)
                _fighters.ForEach(fighter =>
                {
                    // If the fighter's battle meter is already full, no need to send more on battle meter tick events
                    if (fighter.stats.currentBattleMeterValue != 1.0f) TickBattleMeterForFighter(fighter, CoroutineWaitInSeconds);
                });

                yield return new WaitForSeconds(CoroutineWaitInSeconds);
            }
        }
    }
}