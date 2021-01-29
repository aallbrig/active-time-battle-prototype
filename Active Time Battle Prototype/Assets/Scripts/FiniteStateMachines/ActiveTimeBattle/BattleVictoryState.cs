using System;
using Managers;

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
            Context.playerFighters.fighters.ForEach(fighter => fighter.RandomizeBattleMeter());
            // Hide battle victory screen
            Context.ToggleVictoryScreenUI(false);

            base.Leave(callback);
        }
    }
}