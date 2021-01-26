using System;
using System.Collections;
using System.Collections.Generic;
using Data.Actions;
using EventBroker;
using FiniteStateMachines.PlayerBattleInput;
using UI;
using UnityEngine;

namespace Controllers
{
    public class PlayerBattleInputController :
        FsmContextController<PlayerBattleInputState, PlayerBattleInputController>,
        IBattleMeterTick, IPlayerActionSelected, IPlayerTargetsSelected
    {
        public ActiveTimeBattleController atbController;
        
        #region Finite State Machine States

        public PlayerWaitingState PlayerWaitingState;
        public PlayerChooseActionState PlayerChooseActionState;
        public PlayerSelectTargetsState PlayerSelectTargetsState;

        #endregion

        #region User Interface

        public GameObject playerFightersStats;
        public GameObject playerActions;
        public GameObject playerTargets;

        public void TogglePlayerActionsUi(bool value) => ToggleUI(playerActions)(value);
        public void TogglePlayerTargetsUi(bool value) => ToggleUI(playerTargets)(value);
        private Action<bool> ToggleUI(GameObject targetUI) => targetUI.SetActive;

        #endregion

        #region Player Input

        [Serializable]
        public struct PlayerInput
        {
            public FighterController ActiveFighter;
            public FighterAction SelectedAction;
            public List<FighterController> Targets;
        }
        public PlayerInput playerInput;

        #endregion

        private readonly Queue<FighterController> _waitingForPlayerInputQueue = new Queue<FighterController>();

        private IEnumerator _queueCoroutine;
        private IEnumerator WatchQueueCoroutine()
        {
            while (true)
            {
                if (_waitingForPlayerInputQueue.Count > 0 && CurrentState == PlayerWaitingState)
                {
                    playerInput.ActiveFighter = _waitingForPlayerInputQueue.Dequeue();
                    var actions = playerInput.ActiveFighter.GetActions();
                    playerActions.GetComponent<PlayerActions>().SetActions(actions);
                    TransitionToState(PlayerChooseActionState);
                }

                yield return new WaitForSeconds(0.25f);
            }
        }

        private void Start()
        {
            PlayerWaitingState = new PlayerWaitingState(this);
            PlayerChooseActionState = new PlayerChooseActionState(this);
            PlayerSelectTargetsState = new PlayerSelectTargetsState(this);

            TransitionToState(PlayerWaitingState);
        }

        private void OnEnable()
        {
            playerFightersStats.GetComponent<PlayerFightersStats>().SetPlayerFighters(atbController.PlayerFighters);

            EventBroker.EventBroker.Instance.Subscribe((IBattleMeterTick) this);
            EventBroker.EventBroker.Instance.Subscribe((IPlayerActionSelected) this);
            EventBroker.EventBroker.Instance.Subscribe((IPlayerTargetsSelected) this);

            TransitionToState(PlayerWaitingState);
            _queueCoroutine = WatchQueueCoroutine();
            StartCoroutine(_queueCoroutine);
        }

        private void OnDisable()
        {
            EventBroker.EventBroker.Instance.Unsubscribe((IBattleMeterTick) this);
            EventBroker.EventBroker.Instance.Unsubscribe((IPlayerActionSelected) this);
            EventBroker.EventBroker.Instance.Unsubscribe((IPlayerTargetsSelected) this);
            TransitionToState(null);
            StopCoroutine(_queueCoroutine);
        }

        public void NotifyBattleMeterTick(FighterController fighter)
        {
            if (atbController.PlayerFighters.Contains(fighter))
            {
                if (!_waitingForPlayerInputQueue.Contains(fighter) && fighter.stats.currentBattleMeterValue >= 1.0f)
                {
                    _waitingForPlayerInputQueue.Enqueue(fighter);
                }
            }
        }

        public void NotifyPlayerActionSelected(FighterAction action)
        {
            playerInput.SelectedAction = action;
            TransitionToState(PlayerSelectTargetsState);
        }

        public void NotifyPlayerTargetsSelected(List<FighterController> targets)
        {
            playerInput.Targets = targets;
            playerInput.ActiveFighter.ExecuteAction(playerInput.SelectedAction, playerInput.Targets, () =>
            {
                playerInput.ActiveFighter.stats.currentBattleMeterValue = 0f;
                TransitionToState(PlayerWaitingState);
            });
        }
    }
}
