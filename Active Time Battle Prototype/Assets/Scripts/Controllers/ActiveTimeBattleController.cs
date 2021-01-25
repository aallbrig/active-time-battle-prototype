using System;
using System.Collections.Generic;
using ATBFighter;
using EventBroker;
using FiniteStateMachines.ActiveTimeBattle;
using UnityEngine;

namespace Controllers
{
    // TODO: Make this maybe not be a god objective (i.e. move PlayerBattleInputController out?)
    public class ActiveTimeBattleController :
        FsmContextController<ActiveTimeBattleState, ActiveTimeBattleController>,
        IPlayerFighterCreated,
        IEnemyFighterCreated
    {
        public PlayerBattleInputController playerBattleInputController;

        #region Pool of player/enemy fighter prefabs and spawn points

        public List<FighterController> playerFighterPrefabs = new List<FighterController>();
        public List<Transform> playerSpawnPositions = new List<Transform>();
        public List<FighterController> enemyFighterPrefabs = new List<FighterController>();
        public List<Transform> enemySpawnPositions = new List<Transform>();

        #endregion

        #region User Interface References

        public GameObject StartMenuUi;
        public GameObject VictoryScreenUi;
        public GameObject LoseScreenUi;
        public GameObject BattleHUDUi;
        public GameObject BattleAnnoucementsUi;

        #endregion

        #region Finite State Machine States

        public StartMenuState StartMenuState;
        public BeginBattleState BeginBattleState;
        public BattleState BattleState;
        public BattleVictoryState BattleVictoryState;
        public BattleLoseState BattleLoseState;

        #endregion

        #region User Interface Toggles

        public void ToggleStartMenu(bool value) => ToggleUI(StartMenuUi)(value);
        public void ToggleLoseScreenUI(bool value) => ToggleUI(LoseScreenUi)(value);
        public void ToggleVictoryScreenUI(bool value) => ToggleUI(VictoryScreenUi)(value);
        public void ToggleBattleHUDUI(bool value) => ToggleUI(BattleHUDUi)(value);
        public void ToggleBattleAnnouncements(bool value) => ToggleUI(BattleAnnoucementsUi)(value);
        private Action<bool> ToggleUI(GameObject targetUI) => targetUI.SetActive;

        #endregion

        public List<FighterController> playerFighters = new List<FighterController>();
        public List<FighterController> enemyFighters = new List<FighterController>();
        public List<FighterController> fighters = new List<FighterController>();

        private void Start()
        {
            StartMenuState = new StartMenuState(this);
            BeginBattleState = new BeginBattleState(this);
            BattleState = new BattleState(this);
            BattleVictoryState = new BattleVictoryState(this);
            BattleLoseState = new BattleLoseState(this);

            TransitionToState(StartMenuState);

            EventBroker.EventBroker.Instance.Subscribe((IPlayerFighterCreated) this);
            EventBroker.EventBroker.Instance.Subscribe((IEnemyFighterCreated) this);
       }

        private void Update()
        {
            // Hierarchical state machine implementation (HACK)
            // TODO: Find more elegant way of implementing HSM
            // if (playerBattleInputController.CurrentState != null) return;
            CurrentState?.Tick();
        }

        public void NotifyPlayerFighterCreated(FighterController fighter)
        {
            fighters.Add(fighter);
            playerFighters.Add(fighter);
        }

        public void NotifyEnemyFighterCreated(FighterController fighter)
        {
            fighters.Add(fighter);
            enemyFighters.Add(fighter);
        }
    }
}
