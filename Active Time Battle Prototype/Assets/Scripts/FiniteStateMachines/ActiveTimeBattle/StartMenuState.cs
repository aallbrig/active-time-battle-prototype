using System;
using ATBFighter;
using Controllers;
using EventBroker;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class StartMenuState : ActiveTimeBattleState, IStartBattleButtonClicked
    {
        public static event Action<FighterController> OnPlayerFighterCreated; 

        public StartMenuState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.ToggleStartMenu(true);
            // Play start menu on enter animation
            // User is presented with start menu title
            // User is presented with start fight button
            // User is presented with quit game button

            // On start fight button
            EventBroker.EventBroker.Instance.Subscribe(this);
            // On quit game button
        }

        public override void Leave(Action callback)
        {
            EventBroker.EventBroker.Instance.Unsubscribe(this);
            Controller.ToggleStartMenu(false);
            GeneratePlayerCharacters();
            base.Leave(callback);
        }

        private void GeneratePlayerCharacters()
        {
            var playerFightersToSpawnCount = Random.Range(1, Controller.playerSpawnPositions.Count);
            for (int i = 0; i < playerFightersToSpawnCount; i++)
            {
                var randomPlayerFigher =
                    Controller.playerFighterPrefabs[Random.Range(0, Controller.playerFighterPrefabs.Count)];
                var fighter = GameObject.Instantiate(randomPlayerFigher.gameObject, Controller.playerSpawnPositions[i].transform);

                OnPlayerFighterCreated?.Invoke(fighter.GetComponent<FighterController>());
            }
        }

        public void NotifyStartBattleButtonClicked()
        {
            Controller.TransitionToState(Controller.BeginBattleState);
        }
    }
}