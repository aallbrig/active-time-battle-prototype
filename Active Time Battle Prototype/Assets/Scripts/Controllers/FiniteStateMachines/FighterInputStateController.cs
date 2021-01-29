using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using GameEventSystem;
using ScriptableObjects;
using ScriptableObjects.FiniteStateMachines.FighterInput;
using UnityEngine;

namespace Controllers.FiniteStateMachines
{
    public class FighterInputStateController : MonoBehaviour
    {
        [Header("Fighter List")]
        public FighterListRuntimeSet ownFighters;
        public FighterListRuntimeSet opposingFighters;

        [Header("Input Data Container")]
        public FighterInput input;

        [Header("Game Events")]
        public FighterGameEvent activeFighterSet;
        public FighterTargetsGameEvent availableTargets;
        public FighterActionExecuteGameEvent submitFighterInput;

        [Header("Finite State Machine")]
        public State currentState;
        private State _startingState;

        public readonly Queue<FighterController> FighterReadyQueue = new Queue<FighterController>();

        // Player Input
        public void EnqueuePlayerFighter(FighterController fighter)
        {
            if (ownFighters.fighters.Contains(fighter))
                FighterReadyQueue.Enqueue(fighter);
        }

        public void SetActiveFighter(FighterController fighter)
        {
            input.fighter = fighter;
            if (activeFighterSet != null) activeFighterSet.Broadcast(input.fighter);
        }

        public void SetActiveFighterAction(FighterAction action)
        {
            input.action = action;

            var targets = action.actionType == ActionType.Healing ? ownFighters.fighters : opposingFighters.fighters;

            var deadOrAliveTargets = targets.Where(target => 
                action.canBeUsedOnDead ? target.stats.currentHealth <= 0 : target.stats.currentHealth > 0
            ).ToList();

            if (availableTargets != null) availableTargets.Broadcast(deadOrAliveTargets);
        }

        public void SetActiveFighterTargets(List<FighterController> targets) => input.targets = targets;

        public void SubmitFighterInput(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            if (submitFighterInput != null) submitFighterInput.Broadcast(fighter, action, targets);
        }

        public void TransitionToState(State nextState) => currentState = nextState;

        public void ResetState() => currentState = _startingState;
        public void ResetInput() => input.ResetInput();
        private void Update() => currentState.UpdateState(this);

        private void Awake()
        {
            _startingState = currentState;
        }
    }
}
