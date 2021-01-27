﻿using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
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

        public BeginBattleState(ActiveTimeBattleManager manager) : base(manager) {}

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
            Context.enemyFighters.Clear();
            GeneratePlayerEnemies();
            _battleAnnouncements = Context.BattleAnnouncementsUi.GetComponent<BattleAnnouncements>();

            // battle announcements
            _messageQueue.Enqueue($"Enemy count: {Context.enemyFighters.Count}");
            _messageQueue.Enqueue($"Player count: {Context.playerFighters.Count}");
            _processMessageQueueCoroutine = ProcessMessageQueue(() =>
            {
                // After last message, transition to battle state
                Context.TransitionToState(Context.BattleState);
            });

            Context.StartCoroutine(_processMessageQueueCoroutine);
            // (optional) play "battle begin" camera animation
        }

        private void GeneratePlayerEnemies()
        {
            Context.GenerateRandomFighters(Context.enemySpawnPositions, OnEnemyFighterCreated);
        }
    }
}