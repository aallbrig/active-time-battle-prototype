using System;
using Managers;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleLoseState : ActiveTimeBattleState
    {
        public BattleLoseState(ActiveTimeBattleManager manager) : base(manager) {}

        public override void Enter()
        {
            Context.GameOver();
            Context.ToggleLoseScreenUI(true);
        }

        public override void Leave(Action callback)
        {
            Context.ToggleLoseScreenUI(false);

            base.Leave(callback);
        }
    }
}