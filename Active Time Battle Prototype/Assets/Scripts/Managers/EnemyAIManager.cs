using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data;
using GameEventSystem;
using ScriptableObjects;
using ScriptableObjects.Data;
using UnityEngine;
using Utils;

namespace Managers
{
    public class EnemyAIManager : Singleton<EnemyAIManager>
    {
        public FighterActionExecuteGameEvent submitAiInput;
        public FighterListRuntimeSet ownFighters;
        public FighterListRuntimeSet opposingFighters;
        
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
                targets.Add(RandomAliveFighter(ownFighters.fighters));
            }
            else
            {
                targets.Add(RandomAliveFighter(opposingFighters.fighters));
            }
            yield return new WaitForSeconds(Random.Range(ArtificialWaitTimeMin, ArtificialWaitTimeMax));

            if (submitAiInput != null) submitAiInput.Broadcast(fighter, randomAction, targets);
        }

        public void NotifyBattleMeterFull(FighterController fighter)
        {
            if (FighterListsManager.Instance.enemyFighters.fighters.Contains(fighter)) StartCoroutine(HandleEnemyFighterInput(fighter));
        }
    }
}
