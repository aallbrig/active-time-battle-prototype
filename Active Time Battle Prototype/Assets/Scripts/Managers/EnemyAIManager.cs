using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using Controllers;
using Data.Actions;
using EventBroker.SubscriberInterfaces;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemyAIManager : Singleton<EnemyAIManager>, IBattleMeterTick
    {
        public static event Action<ICommand> OnEnemyAiFighterCommand;
        
        public ActiveTimeBattleManager atbManager;
        private const float ArtificialWaitTimeMin = 0.25f;
        private const float ArtificialWaitTimeMax = 1.0f;

        private void Start()
        {
            EventBroker.EventBroker.Instance.Subscribe(Instance);
        }

        protected override void OnDestroy()
        {
            EventBroker.EventBroker.Instance.Unsubscribe(Instance);

            base.OnDestroy();
        }

        private static FighterController RandomFighter(IReadOnlyList<FighterController> fighters) => fighters[Random.Range(0, fighters.Count)];
        private static FighterController RandomAliveFighter(IReadOnlyCollection<FighterController> fighters) =>
            RandomFighter(fighters.Where(fighter => !fighter.stats.dead).ToList());
        private static FighterAction RandomAction(IReadOnlyList<FighterAction> fighterActions) => fighterActions[Random.Range(0, fighterActions.Count)];

        private IEnumerator HandleEnemyFighterInput(FighterController fighter)
        {
            var randomAction = RandomAction(fighter.GetActions());
            yield return new WaitForSeconds(Random.Range(ArtificialWaitTimeMin, ArtificialWaitTimeMax));

            var targets = new List<FighterController>();
            if (randomAction.actionType == ActionType.Healing)
            {
                targets.Add(RandomAliveFighter(atbManager.EnemyFighters));
            }
            else
            {
                targets.Add(RandomAliveFighter(atbManager.PlayerFighters));
            }
            yield return new WaitForSeconds(Random.Range(ArtificialWaitTimeMin, ArtificialWaitTimeMax));

            OnEnemyAiFighterCommand?.Invoke(new BattleCommand(
                fighter,
                randomAction,
                targets,
                fighter.ResetBattleMeter
            ));
        }

        public void NotifyBattleMeterTick(FighterController fighter)
        {
            if (atbManager.EnemyFighters.Contains(fighter) && fighter.stats.currentBattleMeterValue >= 1.0) StartCoroutine(HandleEnemyFighterInput(fighter));
        }
    }
}
