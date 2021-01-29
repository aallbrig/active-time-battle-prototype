using System.Collections.Generic;
using System.Linq;
using Data;
using GameEventSystem;
using Managers;
using ScriptableObjects;
using ScriptableObjects.FiniteStateMachines.FighterInput;
using UnityEngine;

namespace Controllers.FiniteStateMachines
{
    public class FighterInputStateController : MonoBehaviour
    {
        [Header("Input Data Container")]
        public FighterInput input;

        [Header("Game Events")]
        public FighterGameEvent activeFighterSet;
        public FighterTargetsGameEvent possibleTargetList;
        public FighterActionExecuteGameEvent submitFighterInput;

        [Header("Finite State Machine")]
        public State currentState;

        public readonly Queue<FighterController> FighterReadyQueue = new Queue<FighterController>();

        // Player Input
        public void EnqueuePlayerFighter(FighterController fighter)
        {
            if (FighterListsManager.Instance.playerFighters.Contains(fighter))
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

            var targets = action.actionType == ActionType.Healing
                ? FighterListsManager.Instance.playerFighters
                : FighterListsManager.Instance.enemyFighters;

            var deadOrAliveTargets = targets.Where(target => 
                action.canBeUsedOnDead ? target.stats.currentHealth <= 0 : target.stats.currentHealth > 0
            ).ToList();

            if (possibleTargetList != null) possibleTargetList.Broadcast(deadOrAliveTargets);
        }

        public void SetActiveFighterTargets(List<FighterController> targets) => input.targets = targets;

        public void SubmitFighterInput(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            if (submitFighterInput != null) submitFighterInput.Broadcast(fighter, action, targets);
        }

        public void ResetPlayerInput() => input.ResetInput();

        public void TransitionToState(State nextState)
        {
            currentState = nextState;
        }
        private void Update() => currentState.UpdateState(this);
    }
}
