using FiniteStateMachines.PlayerBattleInput;

namespace Controllers
{
    public class PlayerBattleInputController : FsmContextController<PlayerBattleInputState, PlayerBattleInputController>
    {
        private PlayerWaitingState _playerWaitingState;
        private PlayerChooseActionState _playerChooseActionState;
        private PlayerSelectTargetsState _playerSelectTargetsState;

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
            _playerWaitingState = new PlayerWaitingState(this);
            _playerChooseActionState = new PlayerChooseActionState(this);
            _playerSelectTargetsState = new PlayerSelectTargetsState(this);
        }

        private void OnEnable()
        {
            SubscribeToEvents();
            TransitionToState(_playerWaitingState);
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
            TransitionToState(null);
        }
    }
}
