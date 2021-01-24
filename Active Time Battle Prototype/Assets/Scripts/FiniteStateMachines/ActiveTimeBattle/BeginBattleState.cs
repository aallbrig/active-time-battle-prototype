using Controllers;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BeginBattleState : ActiveTimeBattleState
    {
        public BeginBattleState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            GeneratePlayerEnemies();
            // (optional) play "battle begin" camera animation
            // battle announcements
            Controller.ToggleBattleAnnouncements(true);
        }

        public override void Tick()
        {
            // MVP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Controller.TransitionToState(Controller.BattleState);
            }
        }

        private void GeneratePlayerEnemies()
        {
            // Generate player enemies, based on list of enemy prefabs
        }

    }
}