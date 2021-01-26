using System;
using System.Collections.Generic;
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

            var targets = Controller.playerInput.SelectedAction.actionType == ActionType.Healing
                ? atbController.PlayerFighters
                : atbController.EnemyFighters;

            Controller.playerTargets.GetComponent<PlayerTargets>().SetTargets(targets);
            Controller.TogglePlayerTargetsUi(true);
        }
    }
}