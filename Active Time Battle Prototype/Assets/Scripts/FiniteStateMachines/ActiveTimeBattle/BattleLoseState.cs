using System;
using Controllers;
using Managers;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleLoseState : ActiveTimeBattleState
    {
        public BattleLoseState(ActiveTimeBattleManager manager) : base(manager) {}

        public override void Enter()
        {
            Context.ToggleLoseScreenUI(true);
        }

        public override void Leave(Action callback)
        {
            Context.ToggleLoseScreenUI(false);

            base.Leave(callback);
        }
    }
}