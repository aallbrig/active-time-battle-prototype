using System.Collections;
using System.Collections.Generic;
using Controllers;
using Data;
using EventBroker.SubscriberInterfaces;
using UI;
using UnityEngine;
using Utils;

namespace Managers
{
    public class BattleAnnouncementsManager : Singleton<BattleAnnouncementsManager>
    {
        public BattleAnnouncements battleAnnouncementsUi;

        private const float WatchMessageQueueWait = 3.0f;
        private readonly Queue<string> _messageQueue = new Queue<string>();
        private IEnumerator _watchMessageQueueCoroutine;

        private void EnqueueBattleMessage(string message)
        {
            _messageQueue.Enqueue(message);
        }

        private IEnumerator DisplayBattleAnnouncement(string message)
        {
            battleAnnouncementsUi.SetBattleAnnouncement(message);
            battleAnnouncementsUi.Enable();
            yield return new WaitForSeconds(WatchMessageQueueWait);
            battleAnnouncementsUi.Disable();
            yield return new WaitForSeconds(0.5f);
        }

        private IEnumerator WatchMessageQueue()
        {
            while (true)
            {
                if (_messageQueue.Count > 0)
                {
                    var message = _messageQueue.Dequeue();
                    yield return DisplayBattleAnnouncement(message);
                }
                yield return null;
            }
        }

        private void Start()
        {
            _watchMessageQueueCoroutine = WatchMessageQueue();
            StartCoroutine(_watchMessageQueueCoroutine);
        }

        protected override void OnDestroy()
        {
            StopCoroutine(_watchMessageQueueCoroutine);

            base.OnDestroy();
        }

        public void OnFighterActionStart(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            EnqueueBattleMessage(fighter.stats.fighterName + " casts " + action.actionName + " on " + targets.Count + " targets");
        }
    }
}