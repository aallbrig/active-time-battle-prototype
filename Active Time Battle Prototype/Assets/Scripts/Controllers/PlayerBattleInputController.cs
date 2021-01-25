using System;
using System.Collections;
using System.Collections.Generic;
using ATBFighter;
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

        #region Player Input Data

        private struct PlayerInput
        {
            public FighterController activeFighter;
            public ATBFighterAction_SO selectedAction;
            public List<FighterController> targets;
        }

        private PlayerInput playerInput;
        #endregion

        private readonly Queue<FighterController> _waitingForPlayerInputQueue = new Queue<FighterController>();
        public List<FighterController> playerControlledFighters = new List<FighterController>();
        public List<FighterController> enemyFighters = new List<FighterController>();
        public List<FighterController> possibleTargets = new List<FighterController>();

        public void SetPlayerFighters(List<FighterController> fighters)
        {
            playerControlledFighters.Clear();
            playerControlledFighters = fighters;
            playerFightersStats.GetComponent<PlayerFightersStats>().SetPlayerFighters(playerControlledFighters);
        }

        public void SetEnemyFighters(List<FighterController> fighters)
        {
            enemyFighters.Clear();
            enemyFighters = fighters;
        }

        public void SetPossibleActionTargets(List<FighterController> fighters)
        {
            possibleTargets.Clear();
            possibleTargets = fighters;
        }

        private IEnumerator _queueCoroutine;
        private IEnumerator WatchQueueCoroutine()
        {
            while (true)
            {
                if (_waitingForPlayerInputQueue.Count > 0 && CurrentState == PlayerWaitingState)
                {
                    playerInput.activeFighter = _waitingForPlayerInputQueue.Dequeue();
                    playerActions.GetComponent<PlayerActions>().SetActions(playerInput.activeFighter.GetActions());
                    TransitionToState(PlayerChooseActionState);
                }

                yield return new WaitForSeconds(0.25f);
            }
        }

        private void ResetPlayerInput()
        {
            playerInput = new PlayerInput();
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
            if (playerControlledFighters.Contains(fighter))
            {
                if (fighter != playerInput.activeFighter && !_waitingForPlayerInputQueue.Contains(fighter) && fighter.stats.currentBattleMeterValue >= 1.0f)
                {
                    _waitingForPlayerInputQueue.Enqueue(fighter);
                }
            }
        }

        public void NotifyPlayerActionSelected(ATBFighterAction_SO action)
        {
            playerInput.selectedAction = action;
            SetPossibleActionTargets(playerInput.selectedAction.actionType == ActionType.Healing
                ? playerControlledFighters
                : enemyFighters);
            TransitionToState(PlayerSelectTargetsState);
        }

        public void NotifyPlayerTargetsSelected(List<FighterController> targets)
        {
            playerInput.targets = targets;
            playerInput.activeFighter.ExecuteAction(playerInput.selectedAction, playerInput.targets);
            ResetPlayerInput();
            TransitionToState(PlayerWaitingState);
        }
    }
}
