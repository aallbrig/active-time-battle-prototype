using Finite_State_Machines.ActiveTimeBattle;

namespace Controllers
{
    // TODO: Make this maybe not be a god objective (i.e. move PlayerBattleInputController out?)
    public class ActiveTimeBattleController : FSMContextController<ActiveTimeBattleState, ActiveTimeBattleController>
    {
        public PlayerBattleInputController playerBattleInputController;

        public StartMenuState StartMenuState;
        public BeginBattleState BeginBattleState;
        public BattleState BattleState;
        public BattleVictoryState BattleVictoryState;
        public BattleLoseState BattleLoseState;

        private void Start()
        {
            StartMenuState = new StartMenuState(this);
            BeginBattleState = new BeginBattleState(this);
            BattleState = new BattleState(this);
            BattleVictoryState = new BattleVictoryState(this);
            BattleLoseState = new BattleLoseState(this);

            TransitionToState(StartMenuState);
        }

        private void Update() => CurrentState?.Tick();
    }
}
