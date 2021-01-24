using Finite_State_Machines.PlayerBattleInput;

namespace Controllers
{
    public class PlayerBattleInputController : FSMContextController<PlayerBattleInputState, PlayerBattleInputController>
    {
        public PlayerWaitingState PlayerWaitingState;
        public PlayerChooseActionState PlayerChooseActionState;
        public PlayerSelectTargetsState PlayerSelectTargetsState;

        private void Start()
        {
            PlayerWaitingState = new PlayerWaitingState(this);
            PlayerChooseActionState = new PlayerChooseActionState(this);
            PlayerSelectTargetsState = new PlayerSelectTargetsState(this);
        }

        private void OnEnable()
        {
            TransitionToState(PlayerWaitingState);
        }

        private void OnDisable()
        {
            TransitionToState(null);
        }

        private void Update() => CurrentState?.Tick();
    }
}
