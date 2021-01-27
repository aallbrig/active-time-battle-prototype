using System.Linq;
using Data;
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
            var action = Context.playerInput.SelectedAction;

            var targets = action.actionType == ActionType.Healing
                ? FighterListsManager.Instance.playerFighters
                : FighterListsManager.Instance.enemyFighters;

            var deadOrAliveTargets = targets.Where(target => 
                action.canBeUsedOnDead ? target.stats.currentHealth <= 0 : target.stats.currentHealth > 0
            ).ToList();

            Context.SetTargets(deadOrAliveTargets);

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