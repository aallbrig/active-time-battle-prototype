using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using Controllers;
using Managers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleState : ActiveTimeBattleState
    {
        private IEnumerator _battleMeterTickCoroutine;
        private const float CoroutineWaitInSeconds = 0.1f;
        private bool _checkBattleConclusionCondition;
        private List<FighterController> _fighters = new List<FighterController>();

        public BattleState(ActiveTimeBattleManager manager) : base(manager) {}

        public override void Enter()
        {
            _fighters = _fighters.Concat(FighterListsManager.Instance.enemyFighters).ToList();
            _fighters = _fighters.Concat(FighterListsManager.Instance.playerFighters).ToList();

            // Show battle UI HUD
            Context.ToggleBattleHUDUI(true);
            // (optional) animate/set camera to battle state position

            _battleMeterTickCoroutine = BattleMeterTickCoroutine();
            Context.StartCoroutine(_battleMeterTickCoroutine);

            _checkBattleConclusionCondition = true;
        }

        public override void Tick()
        {
            if (!_checkBattleConclusionCondition) return;
            CheckForBattleConclusionCondition();
        }

        public override void Leave(Action callback)
        {
            Context.StartCoroutine(OnLeaveCoroutine(callback));
        }

        private IEnumerator OnLeaveCoroutine(Action callback)
        {
            Context.StopCoroutine(_battleMeterTickCoroutine);
            _fighters = new List<FighterController>();

            // Hide battle UI HUD
            Context.ToggleBattleHUDUI(false);
            Context.ToggleBattleAnnouncements(false);

            yield return new WaitForSeconds(5);

            base.Leave(callback);
        }


        private float SumHealth(List<FighterController> fighters) =>
            fighters.Aggregate(0f, (sum, fighter) => sum + fighter.stats.currentHealth);

        private void CheckForBattleConclusionCondition()
        {
            var enemiesCurrentHealth = SumHealth(FighterListsManager.Instance.enemyFighters);
            var playersCurrentHealth = SumHealth(FighterListsManager.Instance.playerFighters);

            if (enemiesCurrentHealth <= 0)
            {
                _checkBattleConclusionCondition = false;
                Context.TransitionToState(Context.BattleVictoryState);
            }
            else if (playersCurrentHealth <= 0)
            {
                _checkBattleConclusionCondition = false;
                Context.TransitionToState(Context.BattleLoseState);
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

            Context.OnBattleMeterTick(fighter);
        }

        private IEnumerator BattleMeterTickCoroutine()
        {
            while (true)
            {
                // If ATB fighter's battle meter is >= 1.0, trigger "ATB fighter ready to act" event (consumed by player, enemy AI)
                _fighters.Where(fighter => !fighter.stats.dead).ToList().ForEach(fighter =>
                {
                    // If the fighter's battle meter is already full, no need to send more on battle meter tick events
                    if (fighter.stats.currentBattleMeterValue != 1.0f)
                    {
                        TickBattleMeterForFighter(fighter, CoroutineWaitInSeconds);
                    }
                });

                yield return new WaitForSeconds(CoroutineWaitInSeconds);
            }
        }
    }
}