using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data.Actions;
using EventBroker;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerSelectTargetsState: PlayerBattleInputState
    {
        public PlayerSelectTargetsState(PlayerBattleInputController controller) : base(controller) {}

        public override void Enter()
        {
            var atbController = GameObject.FindObjectOfType<ActiveTimeBattleController>();
            var action = Controller.playerInput.SelectedAction;

            var targets = action.actionType == ActionType.Healing
                ? atbController.PlayerFighters
                : atbController.EnemyFighters;

            var deadOrAliveTargets = targets.Where(target => 
                action.actionType == ActionType.Reviving ? target.stats.currentHealth <= 0 : target.stats.currentHealth > 0
            ).ToList();

            Controller.playerTargets.GetComponent<PlayerTargets>().SetTargets(deadOrAliveTargets);
            Controller.TogglePlayerTargetsUi(true);
        }
    }
}