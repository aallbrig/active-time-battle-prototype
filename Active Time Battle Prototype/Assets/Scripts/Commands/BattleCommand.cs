using System;
using System.Collections.Generic;
using Controllers;
using Data;

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
            if (_fighter.stats.dead)
            {
                CommandComplete?.Invoke();
            } else 
            {
                _fighter.ExecuteAction(_fighterAction, _targets);
            }
        }
    }
}