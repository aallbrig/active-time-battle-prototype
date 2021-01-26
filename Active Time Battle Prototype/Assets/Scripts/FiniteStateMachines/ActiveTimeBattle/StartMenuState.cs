using System;
using Controllers;
using EventBroker;
using UnityEditor;
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
            Controller.GenerateRandomFighters(Controller.playerSpawnPositions, OnPlayerFighterCreated);
        }

        public void NotifyStartBattleButtonClicked()
        {
            Controller.TransitionToState(Controller.BeginBattleState);
        }
    }
}