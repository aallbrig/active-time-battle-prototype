using System.Collections;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data;
using EventBroker.SubscriberInterfaces;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Managers
{
    public class BattleCommandQueueProcessor : Singleton<BattleCommandQueueProcessor>, IFighterActionEnqueueRequest
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

        public void NotifyFighterCommand(ICommand fighterCommand)
        {
            _battleCommandQueue.Enqueue(fighterCommand);
        }

        public void ReadyToProcessAnotherBattleCommand(
            FighterController fighter, FighterAction action, List<FighterController> targets
        )
        {
            _readyToProcessAnotherCommand = true;
        }

        private void OnEnable()
        {
            EventBroker.EventBroker.Instance.Subscribe(this);
            _readyToProcessAnotherCommand = true;
            _battleCommandQueueProcessor = CommandQueueProcessorCoroutine();
            StartCoroutine(_battleCommandQueueProcessor);
        }

        protected void OnDisable()
        {
            EventBroker.EventBroker.Instance.Unsubscribe(this);

            StopCoroutine(_battleCommandQueueProcessor);
            _battleCommandQueue.Clear();
        }
    }
}