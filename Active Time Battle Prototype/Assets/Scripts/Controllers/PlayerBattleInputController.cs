using System;
using Finite_State_Machines;
using Finite_State_Machines.PlayerBattleInput;
using UnityEngine;

namespace Controllers
{
    public class PlayerBattleInputController : MonoBehaviour, IFiniteStateMachineContext<PlayerBattleInputState>
    {
        public PlayerWaitingState PlayerWaitingState;
        public PlayerChooseActionState PlayerChooseActionState;
        public PlayerSelectTargetsState PlayerSelectTargetsState;

        public PlayerBattleInputState CurrentState { get; private set; }

        public void TransitionToState(PlayerBattleInputState newState)
        {
            var transitionState = new Action(() =>
            {
                CurrentState = newState;
                CurrentState?.Enter();
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
