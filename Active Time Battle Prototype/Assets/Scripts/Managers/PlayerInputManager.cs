using System;
using System.Collections;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data;
using EventBroker.SubscriberInterfaces;
using FiniteStateMachines;
using FiniteStateMachines.PlayerBattleInput;
using GameEventSystem;
using UI;
using UnityEngine;

namespace Managers
{
    public class PlayerInputManager :
        FiniteStateMachineContext<PlayerBattleInputState, PlayerInputManager>,
        IPlayerActionSelected, IPlayerTargetsSelected
    {
        public static event Action<ICommand> OnPlayerFighterCommand;

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
            this.targetFighters = targets;
            playerTargetsUi.Render(targets);
        }

        public void TogglePlayerActionsUi(bool value) => playerActions.gameObject.SetActive(value);
        public void TogglePlayerTargetsUi(bool value) => playerTargetsUi.gameObject.SetActive(value);

        #endregion

        #region Player Input

        [SerializeField] public FighterController activePlayerFighter;
        [SerializeField] public FighterAction selectedAction;
        [SerializeField] public List<FighterController> targetFighters;

        #endregion

        [Header("Fighter Game Events")]
        public FighterGameEvent playerFighterActive;

        // TODO: Move this queue/processor logic into another script
        private readonly Queue<FighterController> _waitingForPlayerInputQueue = new Queue<FighterController>();
        private IEnumerator _queueCoroutine;

        public void ReEnqueueFighter(FighterController fighter)
        {
            _waitingForPlayerInputQueue.Enqueue(fighter);
        }

        public void ActivatePlayerFighter(FighterController fighter)
        {
            activePlayerFighter = fighter;
            TransitionToState(PlayerChooseActionState);
        }

        public void SetActivePlayerFighterActions(FighterController fighter)
        {
            GameObject.FindObjectOfType<PlayerActions>().SetupActions(fighter);
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
                        if (playerFighterActive != null) playerFighterActive.Broadcast(fighter);
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
            selectedAction = action;
            TransitionToState(PlayerSelectTargetsState);
        }

        public void NotifyPlayerTargetsSelected(List<FighterController> targets)
        {
            TransitionToState(PlayerActionWaitingState);
            targetFighters = targets;

            OnPlayerFighterCommand?.Invoke(new BattleCommand(
                activePlayerFighter,
                selectedAction,
                targetFighters
            ));

            TransitionToState(PlayerWaitingState);
        }
    }
}
