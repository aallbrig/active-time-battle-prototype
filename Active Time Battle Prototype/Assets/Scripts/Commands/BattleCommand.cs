using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data;
using ScriptableObjects;

namespace Commands
{
    public class BattleCommand: ICommand
    {
        public event Action CommandComplete;

        private readonly FighterController _fighter;
        private readonly FighterAction _fighterAction;
        private readonly List<FighterController> _targets;

        public BattleCommand(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            _fighter = fighter;
            _fighterAction = action;
            _targets = targets;
        }

        public void Execute()
        {
            var targetsAlive = _targets.Aggregate(0f, (acc, fighter) => acc + fighter.stats.currentHealth) > 0;
            if (targetsAlive && !_fighter.stats.dead)
            {
                _fighter.ExecuteAction(_fighterAction, _targets);
            } else if (!targetsAlive)
            {
                _fighter.ResetBattleMeter();
            }
        }
    }
}