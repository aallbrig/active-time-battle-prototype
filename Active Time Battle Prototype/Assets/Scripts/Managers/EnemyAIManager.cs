﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using Controllers;
using Data;
using ScriptableObjects;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemyAIManager : Singleton<EnemyAIManager>
    {
        public static event Action<ICommand> OnEnemyAiFighterCommand;
        
        private const float ArtificialWaitTimeMin = 0.25f;
        private const float ArtificialWaitTimeMax = 1.0f;

        private static FighterAction RandomAction(IReadOnlyList<FighterAction> fighterActions) => fighterActions[Random.Range(0, fighterActions.Count)];
        public FighterController RandomAliveFighter(List<FighterController> fighters) {
            var validFighters = fighters.Where(fighter => !fighter.stats.dead).ToList();
            return validFighters[Random.Range(0, validFighters.Count)];
        }

        private IEnumerator HandleEnemyFighterInput(FighterController fighter)
        {
            var randomAction = RandomAction(fighter.GetActions());
            yield return new WaitForSeconds(Random.Range(ArtificialWaitTimeMin, ArtificialWaitTimeMax));

            var targets = new List<FighterController>();
            if (randomAction.actionType == ActionType.Healing)
            {
                targets.Add(RandomAliveFighter(FighterListsManager.Instance.enemyFighters));
            }
            else
            {
                targets.Add(RandomAliveFighter(FighterListsManager.Instance.playerFighters));
            }
            yield return new WaitForSeconds(Random.Range(ArtificialWaitTimeMin, ArtificialWaitTimeMax));

            OnEnemyAiFighterCommand?.Invoke(new BattleCommand(
                fighter,
                randomAction,
                targets
            ));
        }

        public void NotifyBattleMeterFull(FighterController fighter)
        {
            if (FighterListsManager.Instance.enemyFighters.Contains(fighter)) StartCoroutine(HandleEnemyFighterInput(fighter));
        }
    }
}
