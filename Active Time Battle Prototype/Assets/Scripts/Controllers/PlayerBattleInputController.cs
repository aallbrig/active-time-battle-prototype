using System;
using FiniteStateMachines.PlayerBattleInput;
using UnityEngine;

namespace Controllers
{
    public class PlayerBattleInputController : FsmContextController<PlayerBattleInputState, PlayerBattleInputController>
    {
        public GameObject PlayerActions;
        public GameObject PlayerTargets;
        
        public PlayerWaitingState PlayerWaitingState;
        public PlayerChooseActionState PlayerChooseActionState;
        public PlayerSelectTargetsState PlayerSelectTargetsState;

        public void TogglePlayerActionsUi(bool value) => ToggleUI(PlayerActions)(value);
        public void TogglePlayerTargetsUi(bool value) => ToggleUI(PlayerTargets)(value);
        private Action<bool> ToggleUI(GameObject targetUI) => targetUI.SetActive;

        private void SubscribeToEvents()
        {
            // Subscribe to ATB events (i.e. Battle Meter Tick) to show/hide UI
            // Subscribe to player input events
        }

        private void UnsubscribeToEvents()
        {
            // Unsubscribe to ATB events (i.e. Battle Meter Tick) to show/hide UI
            // Unsubscribe to player input events
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
            SubscribeToEvents();
            TransitionToState(PlayerWaitingState);
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
            TransitionToState(null);
        }
    }
}
