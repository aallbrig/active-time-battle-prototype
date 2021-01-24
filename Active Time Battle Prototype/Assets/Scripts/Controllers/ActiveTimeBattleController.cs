using System;
using FiniteStateMachines.ActiveTimeBattle;
using UnityEngine;

namespace Controllers
{
    // TODO: Make this maybe not be a god objective (i.e. move PlayerBattleInputController out?)
    public class ActiveTimeBattleController : FsmContextController<ActiveTimeBattleState, ActiveTimeBattleController>
    {
        public PlayerBattleInputController playerBattleInputController;

        #region User Interface References

        public GameObject VictoryScreenUi;
        public GameObject LoseScreenUi;
        public GameObject BattleHUD;

        #endregion

        public StartMenuState StartMenuState;
        public BeginBattleState BeginBattleState;
        public BattleState BattleState;
        public BattleVictoryState BattleVictoryState;
        public BattleLoseState BattleLoseState;

        public void ToggleLoseScreenUI(bool value) => ToggleUI(LoseScreenUi)(value);
        public void ToggleVictoryScreenUI(bool value) => ToggleUI(VictoryScreenUi)(value);
        public void ToggleBattleHUDUI(bool value) => ToggleUI(BattleHUD)(value);

        private Action<bool> ToggleUI(GameObject TargetUI) => TargetUI.SetActive;

        private void Start()
        {
            StartMenuState = new StartMenuState(this);
            BeginBattleState = new BeginBattleState(this);
            BattleState = new BattleState(this);
            BattleVictoryState = new BattleVictoryState(this);
            BattleLoseState = new BattleLoseState(this);

            TransitionToState(StartMenuState);
        }
    }
}
