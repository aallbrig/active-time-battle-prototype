using System.Linq;
using Controllers;
using Data.Actions;
using Managers;
using UI;
using UnityEngine;

namespace FiniteStateMachines.PlayerBattleInput
{
    public class PlayerSelectTargetsState: PlayerBattleInputState
    {
        public PlayerSelectTargetsState(PlayerInputManager controller) : base(controller) {}

        public override void Enter()
        {
            var atbController = GameObject.FindObjectOfType<ActiveTimeBattleManager>();
            var action = Context.playerInput.SelectedAction;

            var targets = action.actionType == ActionType.Healing
                ? atbController.PlayerFighters
                : atbController.EnemyFighters;

            var deadOrAliveTargets = targets.Where(target => 
                action.canBeUsedOnDead ? target.stats.currentHealth <= 0 : target.stats.currentHealth > 0
            ).ToList();

            Context.playerTargets.GetComponent<PlayerTargets>().SetTargets(deadOrAliveTargets);
            Context.TogglePlayerTargetsUi(true);
        }

        public override void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Context.TogglePlayerTargetsUi(false);
                GameObject.FindObjectOfType<PlayerActions>().EnableButtons();
                Context.TransitionToState(Context.PlayerChooseActionState);
            }
        }
    }
}