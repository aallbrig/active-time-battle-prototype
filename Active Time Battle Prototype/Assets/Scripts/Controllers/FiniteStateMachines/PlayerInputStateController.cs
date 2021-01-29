using System.Collections.Generic;
using System.Linq;
using Data;
using GameEventSystem;
using Managers;
using ScriptableObjects;
using ScriptableObjects.FiniteStateMachines.PlayerInput;
using UnityEngine;

namespace Controllers.FiniteStateMachines
{
    public class PlayerInputStateController : MonoBehaviour
    {
        [Header("Game Events")]
        public FighterGameEvent activeFighterSet;
        public FighterTargetsGameEvent possibleTargetList;
        public FighterActionExecuteGameEvent submitPlayerInput;

        [Header("Finite State Machine")]
        public State currentState;
        [HideInInspector] public FighterController activePlayerFighter;
        [HideInInspector] public FighterAction activePlayerFighterAction;
        [HideInInspector] public List<FighterController> activePlayerFighterTargets = new List<FighterController>();
        public readonly Queue<FighterController> PlayerFighterQueue = new Queue<FighterController>();

        // Player Input
        public void EnqueuePlayerFighter(FighterController fighter)
        {
            if (FighterListsManager.Instance.playerFighters.Contains(fighter))
                PlayerFighterQueue.Enqueue(fighter);
        }

        public void SetActiveFighter(FighterController fighter)
        {
            activePlayerFighter = fighter;
            if (activeFighterSet != null) activeFighterSet.Broadcast(activePlayerFighter);
        }
        public void SetActiveFighterAction(FighterAction action)
        {
            activePlayerFighterAction = action;

            var targets = action.actionType == ActionType.Healing
                ? FighterListsManager.Instance.playerFighters
                : FighterListsManager.Instance.enemyFighters;

            var deadOrAliveTargets = targets.Where(target => 
                action.canBeUsedOnDead ? target.stats.currentHealth <= 0 : target.stats.currentHealth > 0
            ).ToList();
            
            if (possibleTargetList != null) possibleTargetList.Broadcast(deadOrAliveTargets);
        }

        public void SetActiveFighterTargets(List<FighterController> targets) => activePlayerFighterTargets = targets;

        public void SubmitPlayerInput(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            if (submitPlayerInput != null) submitPlayerInput.Broadcast(fighter, action, targets);
        }
        public void ResetPlayerInput()
        {
            activePlayerFighter = null;
            activePlayerFighterAction = null;
            activePlayerFighterTargets = new List<FighterController>();
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != currentState)
            {
                currentState = nextState;
            }
        }
        private void Update()
        {
            currentState.UpdateState(this);
        }
    }
}
