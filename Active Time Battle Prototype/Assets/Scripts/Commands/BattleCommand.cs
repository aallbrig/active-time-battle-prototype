using System;
using System.Collections.Generic;
using Controllers;
using Data.Actions;

namespace Commands
{
    public class BattleCommand: ICommand
    {
        public event Action CommandComplete;

        private readonly FighterController _fighter;
        private readonly FighterAction _fighterAction;
        private readonly List<FighterController> _targets;
        private readonly Action _callback;

        public BattleCommand(FighterController fighter, FighterAction action, List<FighterController> targets, Action callback = null)
        {
            _fighter = fighter;
            _fighterAction = action;
            _targets = targets;
            _callback = () =>
            {
                CommandComplete?.Invoke();
                callback?.Invoke();
            };
        }

        public void Execute()
        {
            _fighter.ExecuteAction(_fighterAction, _targets, _callback);
        }
    }
}