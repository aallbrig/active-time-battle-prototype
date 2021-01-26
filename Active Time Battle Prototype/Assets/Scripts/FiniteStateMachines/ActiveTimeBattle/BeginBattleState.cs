using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UI;
using UnityEngine;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class BeginBattleState : ActiveTimeBattleState
    {
        public static event Action<FighterController> OnEnemyFighterCreated;
        private const float MessageQueueWaitInSeconds = 3.0f;
        private readonly Queue<string> _messageQueue = new Queue<string>();
        private IEnumerator _processMessageQueueCoroutine;
        private BattleAnnouncements _battleAnnouncements;

        public BeginBattleState(ActiveTimeBattleController controller) : base(controller) {}

        private IEnumerator ProcessMessageQueue(Action callback = null)
        {
            // Foreach item in message queue, display it
            while (_messageQueue.Count > 0)
            {
                var message = _messageQueue.Dequeue();
                _battleAnnouncements.SetBattleAnnouncement(message);
                _battleAnnouncements.Enable();
                yield return new WaitForSeconds(MessageQueueWaitInSeconds);
                _battleAnnouncements.ClearBattleAnnouncement();
                _battleAnnouncements.Disable();
            }

            callback?.Invoke();
        }
        
        public override void Enter()
        {
            GeneratePlayerEnemies();
            _battleAnnouncements = Controller.BattleAnnouncementsUi.GetComponent<BattleAnnouncements>();

            // battle announcements
            _messageQueue.Enqueue($"Enemy count: {Controller.EnemyFighters.Count}");
            _messageQueue.Enqueue($"Player count: {Controller.PlayerFighters.Count}");
            _processMessageQueueCoroutine = ProcessMessageQueue(() =>
            {
                // After last message, transition to battle state
                Controller.TransitionToState(Controller.BattleState);
            });

            Controller.StartCoroutine(_processMessageQueueCoroutine);
            // (optional) play "battle begin" camera animation
        }

        private void GeneratePlayerEnemies()
        {
            Controller.GenerateRandomFighters(Controller.enemySpawnPositions, OnEnemyFighterCreated);
        }
    }
}