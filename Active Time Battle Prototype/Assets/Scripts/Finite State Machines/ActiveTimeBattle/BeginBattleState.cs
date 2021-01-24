using Controllers;
using UnityEngine;

namespace Finite_State_Machines.ActiveTimeBattle
{
    public class BeginBattleState : ActiveTimeBattleState
    {
        public BeginBattleState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BattleState);
            }
        }
    }
}