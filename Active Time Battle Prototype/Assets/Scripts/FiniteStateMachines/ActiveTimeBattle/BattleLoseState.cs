using System;
using Controllers;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BattleLoseState : ActiveTimeBattleState
    {
        public BattleLoseState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.ToggleLoseScreenUI(true);
        }

        public override void Leave(Action callback)
        {
            Controller.ToggleLoseScreenUI(false);

            base.Leave(callback);
        }
    }
}