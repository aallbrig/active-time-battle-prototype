using System;
using Managers;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class StartMenuState : ActiveTimeBattleState
    {
        public StartMenuState(ActiveTimeBattleManager manager) : base(manager) {}

        public override void Enter()
        {
            Context.battleOver.Broadcast();
            Context.ToggleStartMenu(true);
        }

        public override void Leave(Action callback)
        {
            Context.battleStart.Broadcast();
            Context.ToggleStartMenu(false);
            base.Leave(callback);
        }
    }
}