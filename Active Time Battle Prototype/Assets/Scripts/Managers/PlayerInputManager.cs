using System;
using System.Collections;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data;
using EventBroker.SubscriberInterfaces;
using FiniteStateMachines;
using FiniteStateMachines.PlayerBattleInput;
using UI;
using UnityEngine;

namespace Managers
{
    public class PlayerInputManager :
        FiniteStateMachineContext<PlayerBattleInputState, PlayerInputManager>,
        IPlayerActionSelected, IPlayerTargetsSelected
    {
        public static event Action<FighterController> OnSetPlayerActiveFighter;
        public static event Action<ICommand> OnPlayerFighterCommand;

        public ActiveTimeBattleManager atbManager;
        
        #region Finite State Machine States

        public PlayerWaitingState PlayerWaitingState;
        public PlayerChooseActionState PlayerChooseActionState;
        public PlayerSelectTargetsState PlayerSelectTargetsState;
        public PlayerActionWaitingState PlayerActionWaitingState;

        #endregion

        #region User Interface

        public GameObject playerActions;
        public PlayerTargets playerTargetsUi;

        public void SetTargets(List<FighterController> targets)
        {
            // TODO: fire off scriptable object event?
            playerInput.Targets = targets;
            playerTargetsUi.Render(playerInput.Targets);
        }

        public void TogglePlayerActionsUi(bool value) => playerActions.gameObject.SetActive(value);
        public void TogglePlayerTargetsUi(bool value) => playerTargetsUi.gameObject.SetActive(value);

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

        public void ReEnqueueFighter(FighterController fighter)
        {
            _waitingForPlayerInputQueue.Enqueue(fighter);
        }

        private IEnumerator WatchQueueCoroutine()
        {
            while (true)
            {
                if (_waitingForPlayerInputQueue.Count > 0 && CurrentState == PlayerWaitingState)
                {
                    var fighter = _waitingForPlayerInputQueue.Dequeue();
                    if (!fighter.stats.dead)
                    {
                        playerInput.ActiveFighter = fighter;
                        OnSetPlayerActiveFighter?.Invoke(playerInput.ActiveFighter);
                        var actions = playerInput.ActiveFighter.GetActions();
                        playerActions.GetComponent<PlayerActions>().SetActions(actions);
                        TransitionToState(PlayerChooseActionState);
                    }
                }

                yield return new WaitForSeconds(0.25f);
            }
        }

        private void Update() => CurrentState?.Tick();

        private void Start()
        {
            PlayerWaitingState = new PlayerWaitingState(this);
            PlayerChooseActionState = new PlayerChooseActionState(this);
            PlayerSelectTargetsState = new PlayerSelectTargetsState(this);
            PlayerActionWaitingState = new PlayerActionWaitingState(this);

            TransitionToState(PlayerWaitingState);
        }

        private void OnEnable()
        {
            EventBroker.EventBroker.Instance.Subscribe((IPlayerActionSelected) this);
            EventBroker.EventBroker.Instance.Subscribe((IPlayerTargetsSelected) this);

            TransitionToState(PlayerWaitingState);
            _queueCoroutine = WatchQueueCoroutine();
            StartCoroutine(_queueCoroutine);
        }

        private void OnDisable()
        {
            EventBroker.EventBroker.Instance.Unsubscribe((IPlayerActionSelected) this);
            EventBroker.EventBroker.Instance.Unsubscribe((IPlayerTargetsSelected) this);
            TransitionToState(null);
            StopCoroutine(_queueCoroutine);
        }

        public void EnqueuePlayerFighter(FighterController fighter)
        {
            if (!FighterListsManager.Instance.playerFighters.Contains(fighter)) return;

            if (!_waitingForPlayerInputQueue.Contains(fighter)) _waitingForPlayerInputQueue.Enqueue(fighter);
        }

        public void NotifyPlayerActionSelected(FighterAction action)
        {
            playerInput.SelectedAction = action;
            TransitionToState(PlayerSelectTargetsState);
        }

        public void NotifyPlayerTargetsSelected(List<FighterController> targets)
        {
            TransitionToState(PlayerActionWaitingState);
            playerInput.Targets = targets;

            OnPlayerFighterCommand?.Invoke(new BattleCommand(
                playerInput.ActiveFighter,
                playerInput.SelectedAction,
                playerInput.Targets,
                () =>
                {
                    playerInput.ActiveFighter.ResetBattleMeter();
                }
            ));

            TransitionToState(PlayerWaitingState);
        }
    }
}
