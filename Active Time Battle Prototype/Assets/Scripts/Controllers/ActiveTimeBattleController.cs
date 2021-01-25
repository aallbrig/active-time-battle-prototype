using System;
using System.Collections.Generic;
using System.Linq;
using ATBFighter;
using EventBroker;
using FiniteStateMachines.ActiveTimeBattle;
using UnityEngine;

namespace Controllers
{
    // TODO: Make this maybe not be a god objective (i.e. move PlayerBattleInputController out?)
    public class ActiveTimeBattleController : FsmContextController<ActiveTimeBattleState, ActiveTimeBattleController>,
        IPlayerFighterCreated, IEnemyFighterCreated, IContinueBattlingButtonClicked, IQuitButtonClicked, IRestartButtonClicked
    {
        public PlayerBattleInputController playerBattleInputController;

        #region Pool of player/enemy fighter prefabs and spawn points

        public List<FighterController> playerFighterPrefabs = new List<FighterController>();
        public List<Transform> playerSpawnPositions = new List<Transform>();
        public List<FighterController> enemyFighterPrefabs = new List<FighterController>();
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

        #endregion

        private void Start()
        {
            // Initialize states
            StartMenuState = new StartMenuState(this);
            BeginBattleState = new BeginBattleState(this);
            BattleState = new BattleState(this);
            BattleVictoryState = new BattleVictoryState(this);
            BattleLoseState = new BattleLoseState(this);

            // Setup subscriptions to notable events
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
