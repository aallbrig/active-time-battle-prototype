using System;
using System.Collections.Generic;
using ATBFighter;
using Controllers;
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
            Controller.playerTargets.GetComponent<PlayerTargets>().SetTargets(Controller.possibleTargets);
            Controller.TogglePlayerTargetsUi(true);
        }

        public override void Leave(Action callback)
        {
            // Controller.ActiveFighter.ResetBattleMeter();
            // Controller.ActiveFighter = null;
            // Controller.SelectedAction = null;
            // Controller.Targets = null;
            base.Leave(callback);
        }

    }
}