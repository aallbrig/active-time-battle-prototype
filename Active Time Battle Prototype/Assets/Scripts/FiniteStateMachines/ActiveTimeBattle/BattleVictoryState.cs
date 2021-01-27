using System;
using Controllers;
using Managers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleVictoryState : ActiveTimeBattleState
    {
        public BattleVictoryState(ActiveTimeBattleManager manager) : base(manager) {}

        public override void Enter()
        {
            // Show battle victory screen
            Context.ToggleVictoryScreenUI(true);
        }

        public override void Leave(Action callback)
        {
            FighterListsManager.Instance.playerFighters.ForEach(fighter => fighter.RandomizeBattleMeter());
            // Hide battle victory screen
            Context.ToggleVictoryScreenUI(false);

            base.Leave(callback);
        }
    }
}