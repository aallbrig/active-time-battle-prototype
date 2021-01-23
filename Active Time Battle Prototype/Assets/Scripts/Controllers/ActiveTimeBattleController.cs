using System;
using Finite_State_Machines;
using Finite_State_Machines.ActiveTimeBattle;
using UnityEngine;

namespace Controllers
{
    public class ActiveTimeBattleController : MonoBehaviour, IFiniteStateMachineContext<ActiveTimeBattleState>
    {
        public StartMenuState StartMenuState;
        public BeginBattleState BeginBattleState;
        public BattleState BattleState;
        public BattleVictoryState BattleVictoryState;
        public BattleLoseState BattleLoseState;

        public ActiveTimeBattleState CurrentState { get; private set; }

        public void TransitionToState(ActiveTimeBattleState newState)
        {
            var transitionState = new Action(() =>
            {
                CurrentState = newState;
                CurrentState.Enter();
            });

            if (CurrentState != null)
            {
                StartCoroutine(CurrentState.Leave(transitionState));
            }
            else
            {
                transitionState();
            }
        }

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
