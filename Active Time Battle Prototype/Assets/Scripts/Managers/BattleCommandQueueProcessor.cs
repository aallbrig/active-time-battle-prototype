using System.Collections;
using System.Collections.Generic;
using Commands;
using Controllers;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Managers
{
    public class BattleCommandQueueProcessor : Singleton<BattleCommandQueueProcessor>
    {
        public FighterListRuntimeSet playerFighters;
        public FighterListRuntimeSet enemyFighters;

        private readonly Queue<ICommand> _battleCommandQueue = new Queue<ICommand>();
        private readonly List<FighterController> _fightersTakingAction = new List<FighterController>();
        private IEnumerator _battleCommandQueueProcessor;

        private IEnumerator CommandQueueProcessorCoroutine()
        {
            while (true)
            {
                if (_fightersTakingAction.Count == 0 && _battleCommandQueue.Count > 0)
                {
                    var cmds = GetAllCommandsOfSameFighterFaction((BattleCommand) _battleCommandQueue.Dequeue());
                    cmds.ForEach(cmd => cmd.Execute());
                }
                yield return new WaitForSeconds(0.25f);
            }
        }

        public void EnqueueFighterAction(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            _battleCommandQueue.Enqueue(new BattleCommand(fighter, action, targets));
        }

        public void OnFighterActionStart(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            if (!_fightersTakingAction.Contains(fighter)) _fightersTakingAction.Add(fighter);
        }

        public void OnFighterActionComplete(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            if (_fightersTakingAction.Contains(fighter)) _fightersTakingAction.Remove(fighter);
        }

        private List<BattleCommand> GetAllCommandsOfSameFighterFaction(BattleCommand cmd)
        {
            var cmds = new List<BattleCommand> {cmd};

            var fighter = cmd.Fighter;
            List<FighterController> fighterList = null;
            if (playerFighters.fighters.Contains(fighter))
            {
                fighterList = playerFighters.fighters;
            } else if (enemyFighters.fighters.Contains(fighter))
            {
                fighterList = enemyFighters.fighters;
            }

            while (_battleCommandQueue.Count > 0)
            {
                var nextCmd = (BattleCommand) _battleCommandQueue.Peek();
                if (fighterList.Contains(nextCmd.Fighter))
                {
                    cmds.Add((BattleCommand) _battleCommandQueue.Dequeue());
                }
                else
                {
                    break;
                }
            }

            return cmds;
        }

        private void OnEnable()
        {
            _battleCommandQueueProcessor = CommandQueueProcessorCoroutine();
            StartCoroutine(_battleCommandQueueProcessor);
        }

        protected void OnDisable()
        {
            _battleCommandQueue.Clear();
            _fightersTakingAction.Clear();
            StopCoroutine(_battleCommandQueueProcessor);
            _battleCommandQueue.Clear();
        }
    }
}