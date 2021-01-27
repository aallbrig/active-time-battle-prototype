using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using EventBroker.SubscriberInterfaces;
using FiniteStateMachines.ActiveTimeBattle;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    // TODO: Make this maybe not be a god objective (i.e. move PlayerBattleInputController out?)
    public class ActiveTimeBattleManager : FsmContextController<ActiveTimeBattleState, ActiveTimeBattleManager>,
        IPlayerFighterCreated, IEnemyFighterCreated, IContinueBattlingButtonClicked, IQuitButtonClicked, IRestartButtonClicked,
        IStartBattleButtonClicked
    {
        public static event Action<FighterController> OnPlayerFighterCreated; 

        public PlayerInputManager playerInputManager;

        #region Pool of player/enemy fighter spawn points

        public List<Transform> playerSpawnPositions = new List<Transform>();
        public List<Transform> enemySpawnPositions = new List<Transform>();

        #endregion

        #region User Interface References and Toggles

        // References
        public GameObject StartMenuUi;
        public GameObject VictoryScreenUi;
        public GameObject LoseScreenUi;
        public GameObject BattleHUDUi;
        public GameObject BattleAnnouncementsUi;

        // Toggles
        public void ToggleStartMenu(bool value) => ToggleUI(StartMenuUi)(value);
        public void ToggleLoseScreenUI(bool value) => ToggleUI(LoseScreenUi)(value);
        public void ToggleVictoryScreenUI(bool value) => ToggleUI(VictoryScreenUi)(value);
        public void ToggleBattleHUDUI(bool value) => ToggleUI(BattleHUDUi)(value);
        public void ToggleBattleAnnouncements(bool value) => ToggleUI(BattleAnnouncementsUi)(value);
        private Action<bool> ToggleUI(GameObject targetUI) => targetUI.SetActive;

        #endregion

        #region Finite State Machine States

        public StartMenuState StartMenuState;
        public BeginBattleState BeginBattleState;
        public BattleState BattleState;
        public BattleVictoryState BattleVictoryState;
        public BattleLoseState BattleLoseState;

        #endregion

        #region Lists of fighters and whom they belong to

        public readonly List<FighterController> PlayerFighters = new List<FighterController>();
        public readonly List<FighterController> EnemyFighters = new List<FighterController>();

        private void ClearFighters(List<FighterController> fighters)
        {
            fighters.ForEach(fighter => Destroy(fighter.gameObject));
            fighters.Clear();
        }

        #endregion

        #region EventBroker Subscriptions

        public void NotifyPlayerFighterCreated(FighterController fighter) => PlayerFighters.Add(fighter);
        public void NotifyEnemyFighterCreated(FighterController fighter) => EnemyFighters.Add(fighter);
        public void NotifyContinueBattlingButtonClick()
        {
            ClearFighters(EnemyFighters);

            TransitionToState(BeginBattleState);
        }

        public void NotifyQuitButtonClicked()
        {
            ClearFighters(EnemyFighters);
            ClearFighters(PlayerFighters);

            TransitionToState(StartMenuState);
        }

        public void NotifyRestartButtonClicked()
        {
            ClearFighters(EnemyFighters);
            ClearFighters(PlayerFighters);

            TransitionToState(StartMenuState);
        }

        public void NotifyStartBattleButtonClicked()
        {
            GeneratePlayerCharacters();
            TransitionToState(BeginBattleState);
        }

        private void GeneratePlayerCharacters()
        {
            GenerateRandomFighters(playerSpawnPositions, OnPlayerFighterCreated);
        }

        #endregion

        private List<GameObject> GetAllFightersAssetPath() => Resources.LoadAll<GameObject>("Fighters").ToList();

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
            EventBroker.EventBroker.Instance.Subscribe((IPlayerFighterCreated) this);
            EventBroker.EventBroker.Instance.Subscribe((IEnemyFighterCreated) this);
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
