﻿using System.Collections.Generic;
using Commands;
using Controllers;
using Data;
using EventBroker.SubscriberInterfaces;
using Managers;
using UI;
using UnityEngine;

namespace EventBroker
{
    public class EventBroker : MonoBehaviour,
        IEventBroker<IPlayerActionSelected>, IPlayerActionSelected,
        IEventBroker<IPlayerTargetsSelected>, IPlayerTargetsSelected,
        IEventBroker<IFighterAction>, IFighterAction,
        IEventBroker<IFighterTakeDamage>, IFighterTakeDamage,
        IEventBroker<IFighterHeal>, IFighterHeal,
        IEventBroker<IFighterActionEnqueueRequest>, IFighterActionEnqueueRequest
    {
        public static EventBroker Instance { get; private set; }

        #region Subscriber lists

        private readonly List<IPlayerActionSelected> _playerActionSelectedSubscribers = new List<IPlayerActionSelected>();
        private readonly List<IPlayerTargetsSelected> _playerTargetsSelectedSubscribers = new List<IPlayerTargetsSelected>();
        private readonly List<IFighterAction> _fighterActionSubscribers = new List<IFighterAction>();
        private readonly List<IFighterTakeDamage> _fighterTakeDamageSubscribers = new List<IFighterTakeDamage>();
        private readonly List<IFighterHeal> _fighterHealSubscribers = new List<IFighterHeal>();
        private readonly List<IFighterActionEnqueueRequest> _fighterCommandSubscribers = new List<IFighterActionEnqueueRequest>();


        List<IPlayerActionSelected> IEventBroker<IPlayerActionSelected>.Subscribers => _playerActionSelectedSubscribers;
        List<IPlayerTargetsSelected> IEventBroker<IPlayerTargetsSelected>.Subscribers => _playerTargetsSelectedSubscribers;
        List<IFighterAction> IEventBroker<IFighterAction>.Subscribers => _fighterActionSubscribers;
        List<IFighterTakeDamage> IEventBroker<IFighterTakeDamage>.Subscribers => _fighterTakeDamageSubscribers;
        List<IFighterHeal> IEventBroker<IFighterHeal>.Subscribers => _fighterHealSubscribers;
        List<IFighterActionEnqueueRequest> IEventBroker<IFighterActionEnqueueRequest>.Subscribers =>
            _fighterCommandSubscribers;

        #endregion


        public void Subscribe(IFighterActionEnqueueRequest subscriber) =>
            _fighterCommandSubscribers.Add(subscriber);
        public void Unsubscribe(IFighterActionEnqueueRequest subscriber) =>
            _fighterCommandSubscribers.Remove(subscriber);
        public void NotifyFighterCommand(ICommand fighterCommand) =>
            _fighterCommandSubscribers.ForEach(sub => sub.NotifyFighterCommand(fighterCommand));

        public void Subscribe(IFighterHeal subscriber) => _fighterHealSubscribers.Add(subscriber);
        public void Unsubscribe(IFighterHeal subscriber) => _fighterHealSubscribers.Remove(subscriber);
        public void NotifyFighterHeal(FighterController fighter, float heal) =>
            _fighterHealSubscribers.ForEach(sub => sub.NotifyFighterHeal(fighter, heal));

        public void Subscribe(IFighterTakeDamage subscriber) => _fighterTakeDamageSubscribers.Add(subscriber);
        public void Unsubscribe(IFighterTakeDamage subscriber) => _fighterTakeDamageSubscribers.Remove(subscriber);
        public void NotifyFighterTakeDamage(FighterController fighter, float damage) =>
            _fighterTakeDamageSubscribers.ForEach(sub => sub.NotifyFighterTakeDamage(fighter, damage));

        public void Subscribe(IFighterAction subscriber) => _fighterActionSubscribers.Add(subscriber);
        public void Unsubscribe(IFighterAction subscriber) => _fighterActionSubscribers.Remove(subscriber);
        public void NotifyFighterAction(FighterController fighter, FighterAction action, List<FighterController> targets) =>
            _fighterActionSubscribers.ForEach(sub => sub.NotifyFighterAction(fighter, action, targets));

        public void Subscribe(IPlayerTargetsSelected subscriber) =>
            _playerTargetsSelectedSubscribers.Add(subscriber);
        public void Unsubscribe(IPlayerTargetsSelected subscriber) =>
            _playerTargetsSelectedSubscribers.Remove(subscriber);
        public void NotifyPlayerTargetsSelected(List<FighterController> targets) =>
            _playerTargetsSelectedSubscribers.ForEach(sub => sub.NotifyPlayerTargetsSelected(targets));

        public void Subscribe(IPlayerActionSelected subscriber) =>
            _playerActionSelectedSubscribers.Add(subscriber);
        public void Unsubscribe(IPlayerActionSelected subscriber) =>
            _playerActionSelectedSubscribers.Remove(subscriber);
        public void NotifyPlayerActionSelected(FighterAction action) =>
            _playerActionSelectedSubscribers.ForEach(sub => sub.NotifyPlayerActionSelected(action));


        private void SubscribeToEvents()
        {
            PlayerActions.OnPlayerActionButtonClick += NotifyPlayerActionSelected;
            PlayerTargets.OnPlayerTargetButtonClick += NotifyPlayerTargetsSelected;
            FighterController.OnFighterAction += NotifyFighterAction;
            FighterController.OnFighterTakeDamage += NotifyFighterTakeDamage;
            FighterController.OnFighterHeal += NotifyFighterHeal;
            PlayerInputManager.OnPlayerFighterCommand += NotifyFighterCommand;
            EnemyAIManager.OnEnemyAiFighterCommand += NotifyFighterCommand;
        }

        private void UnsubscribeToEvents()
        {
            PlayerActions.OnPlayerActionButtonClick -= NotifyPlayerActionSelected;
            PlayerTargets.OnPlayerTargetButtonClick -= NotifyPlayerTargetsSelected;
            FighterController.OnFighterAction -= NotifyFighterAction;
            FighterController.OnFighterTakeDamage -= NotifyFighterTakeDamage;
            FighterController.OnFighterHeal -= NotifyFighterHeal;
            PlayerInputManager.OnPlayerFighterCommand -= NotifyFighterCommand;
            EnemyAIManager.OnEnemyAiFighterCommand -= NotifyFighterCommand;
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.Log("[Singleton] More than one event broker singleton being instantiated");
        }

        private void Start() => SubscribeToEvents();
        private void OnDestroy() => UnsubscribeToEvents();
    }
}