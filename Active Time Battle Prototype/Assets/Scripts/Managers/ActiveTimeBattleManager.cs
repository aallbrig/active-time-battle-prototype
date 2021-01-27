using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data;
using EventBroker.SubscriberInterfaces;
using FiniteStateMachines;
using FiniteStateMachines.ActiveTimeBattle;
using GameEventSystem;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    // TODO: Make this maybe not be a god objective (i.e. move PlayerBattleInputController out?)
    public class ActiveTimeBattleManager : FiniteStateMachineContext<ActiveTimeBattleState, ActiveTimeBattleManager>,
        IContinueBattlingButtonClicked, IQuitButtonClicked, IRestartButtonClicked,
        IStartBattleButtonClicked
    {
        public static event Action<FighterController> OnPlayerFighterCreated; 
        public static event Action<FighterController> OnEnemyFighterCreated;


        public PlayerInputManager playerInputManager;

        #region Pool of player/enemy fighter spawn points

        public List<Transform> playerSpawnPositions = new List<Transform>();
        public List<Transform> enemySpawnPositions = new List<Transform>();

        #endregion

        #region User Interface References and Toggles

        // References
        public UnityEngine.GameObject StartMenuUi;
        public UnityEngine.GameObject VictoryScreenUi;
        public UnityEngine.GameObject LoseScreenUi;
        public UnityEngine.GameObject BattleHUDUi;
        public UnityEngine.GameObject BattleAnnouncementsUi;

        // Toggles
        public void ToggleStartMenu(bool value) => ToggleUI(StartMenuUi)(value);
        public void ToggleLoseScreenUI(bool value) => ToggleUI(LoseScreenUi)(value);
        public void ToggleVictoryScreenUI(bool value) => ToggleUI(VictoryScreenUi)(value);
        public void ToggleBattleHUDUI(bool value) => ToggleUI(BattleHUDUi)(value);
        public void ToggleBattleAnnouncements(bool value) => ToggleUI(BattleAnnouncementsUi)(value);
        private Action<bool> ToggleUI(UnityEngine.GameObject targetUI) => targetUI.SetActive;

        #endregion

        #region Finite State Machine States

        public StartMenuState StartMenuState;
        public BeginBattleState BeginBattleState;
        public BattleState BattleState;
        public BattleVictoryState BattleVictoryState;
        public BattleLoseState BattleLoseState;

        #endregion


        [Header("Fighter Game Events")]
        public FighterGameEvent playerFighterCreated;
        public FighterGameEvent playerFighterDeleted;
        public FighterGameEvent enemyFighterCreated;
        public FighterGameEvent enemyFighterDeleted;
        public FighterGameEvent fighterBattleMeterTick;
        public FighterGameEvent fighterBattleMeterFull;

        #region EventBroker Subscriptions

        public void NotifyContinueBattlingButtonClick()
        {
            ClearEnemyFighters();

            TransitionToState(BeginBattleState);
        }

        public void NotifyQuitButtonClicked()
        {
            ClearEnemyFighters();
            ClearPlayerFighters();

            TransitionToState(StartMenuState);
        }

        public void NotifyRestartButtonClicked()
        {
            ClearEnemyFighters();
            ClearPlayerFighters();

            TransitionToState(StartMenuState);
        }

        public void NotifyStartBattleButtonClicked()
        {
            GeneratePlayerCharacters();
            TransitionToState(BeginBattleState);
        }

        private void GeneratePlayerCharacters()
        {
            GenerateRandomFighters(playerSpawnPositions, fighter =>
            {
                // Brand new system
                if (playerFighterCreated != null) playerFighterCreated.Broadcast(fighter);

                // TODO: fully replace one of the two systems
                OnPlayerFighterCreated?.Invoke(fighter);
            });
        }

        public void GeneratePlayerEnemies()
        {
            GenerateRandomFighters(enemySpawnPositions, fighter =>
            {
                // Brand new system
                if (enemyFighterCreated != null) enemyFighterCreated.Broadcast(fighter);
                OnEnemyFighterCreated?.Invoke(fighter);
            });
        }

        public void ClearEnemyFighters()
        {
            if (enemyFighterDeleted != null)
                FighterListsManager.Instance.enemyFighters.ForEach(enemyFighterDeleted.Broadcast);

            FighterListsManager.Instance.ClearEnemyFighters();
        }

        private void ClearPlayerFighters()
        {
            if (playerFighterDeleted != null)
                FighterListsManager.Instance.playerFighters.ForEach(playerFighterDeleted.Broadcast);

            FighterListsManager.Instance.ClearPlayerFighters();
        }
        #endregion

        private List<GameObject> GetAllFightersAssetPath() => Resources.LoadAll<GameObject>("Fighters").ToList();

        public void OnBattleMeterTick(FighterController fighter)
        {
            if (fighterBattleMeterTick != null) fighterBattleMeterTick.Broadcast(fighter);

            if (fighter.stats.currentBattleMeterValue >= 1.0f)
            {
                if (fighterBattleMeterFull != null) fighterBattleMeterFull.Broadcast(fighter);
            }
        }

        public void GenerateRandomFighters(List<Transform> spawnPositions, Action<FighterController> callback)
        {
            var numberOfFightersToSpawn = Random.Range(1, spawnPositions.Count);
            var allFighters = GetAllFightersAssetPath();
            var shuffledSpawnPoints = spawnPositions.Shuffle();

            for (var i = numberOfFightersToSpawn; i > 0; i--)
            {
                var randomFighterPrefab = allFighters[Random.Range(0, allFighters.Count)];
                var fighter = Instantiate(randomFighterPrefab, shuffledSpawnPoints[i].transform);
                var fighterController = fighter.GetComponent<FighterController>();

                callback?.Invoke(fighterController);
            }
        }

        private void Start()
        {
            // Initialize states
            StartMenuState = new StartMenuState(this);
            BeginBattleState = new BeginBattleState(this);
            BattleState = new BattleState(this);
            BattleVictoryState = new BattleVictoryState(this);
            BattleLoseState = new BattleLoseState(this);

            // Setup subscriptions to notable events
            EventBroker.EventBroker.Instance.Subscribe((IStartBattleButtonClicked) this);
            EventBroker.EventBroker.Instance.Subscribe((IContinueBattlingButtonClicked) this);
            EventBroker.EventBroker.Instance.Subscribe((IRestartButtonClicked) this);
            EventBroker.EventBroker.Instance.Subscribe((IQuitButtonClicked) this);

            // Initial state
            TransitionToState(StartMenuState);
        }

        private void Update()
        {
            // Hierarchical state machine implementation (HACK)
            // TODO: Find more elegant way of implementing HSM
            // if (playerBattleInputController.CurrentState != null) return;
            CurrentState?.Tick();
        }
    }
}
