using System;
using ATBFighter;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BeginBattleState : ActiveTimeBattleState
    {
        public static event Action<FighterController> OnEnemyFighterCreated;

        public BeginBattleState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            GeneratePlayerEnemies();
            // Controller.playerBattleInputController.SetPlayerFighters();
            // (optional) play "battle begin" camera animation
            // battle announcements
            Controller.ToggleBattleAnnouncements(true);
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BattleState);
            }
        }

        private void GeneratePlayerEnemies()
        {
            var enemyFightersToSpawn = Random.Range(1, Controller.enemySpawnPositions.Count);
            for (int i = 0; i < enemyFightersToSpawn; i++)
            {
                var randomEnemyFighterPrefab = Controller.enemyFighterPrefabs[Random.Range(0, Controller.enemyFighterPrefabs.Count)];
                var fighter = GameObject.Instantiate(randomEnemyFighterPrefab.gameObject, Controller.enemySpawnPositions[i].transform);

                OnEnemyFighterCreated?.Invoke(fighter.GetComponent<FighterController>());
            }
        }
    }
}