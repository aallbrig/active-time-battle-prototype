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
        private bool _readyToProcessAnotherCommand = true;
        private readonly Queue<ICommand> _battleCommandQueue = new Queue<ICommand>();
        private IEnumerator _battleCommandQueueProcessor;

        private IEnumerator CommandQueueProcessorCoroutine()
        {
            while (true)
            {
                if (_readyToProcessAnotherCommand && _battleCommandQueue.Count > 0)
                {
                    _readyToProcessAnotherCommand = false;

                    var cmd = _battleCommandQueue.Dequeue();
                    cmd.Execute();
                }
                yield return new WaitForSeconds(0.25f);
            }
        }

        public void EnqueueFighterAction(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            _battleCommandQueue.Enqueue(new BattleCommand(fighter, action, targets));
        }

        public void ReadyToProcessAnotherBattleCommand(
            FighterController fighter, FighterAction action, List<FighterController> targets
        )
        {
            _readyToProcessAnotherCommand = true;
        }

        private void OnEnable()
        {
            _readyToProcessAnotherCommand = true;
            _battleCommandQueueProcessor = CommandQueueProcessorCoroutine();
            StartCoroutine(_battleCommandQueueProcessor);
        }

        protected void OnDisable()
        {
            StopCoroutine(_battleCommandQueueProcessor);
            _battleCommandQueue.Clear();
        }
    }
}