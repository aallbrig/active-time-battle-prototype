using System;
using System.Collections.Generic;
using ATBFighter;
using FiniteStateMachines.ActiveTimeBattle;
using UI;
using UnityEngine;

namespace EventBroker
{
    public class EventBroker : MonoBehaviour,
        IEventBroker<IBattleMeterTick>, IBattleMeterTick,
        IEventBroker<IPlayerActionSelected>, IPlayerActionSelected,
        IEventBroker<IPlayerTargetsSelected>, IPlayerTargetsSelected
    {
        private readonly List<IBattleMeterTick> _battleMeterSubscribers = new List<IBattleMeterTick>();
        private readonly List<IPlayerActionSelected> _playerActionSelectedSubscribers = new List<IPlayerActionSelected>();
        private readonly List<IPlayerTargetsSelected> _playerTargetsSelectedSubscriber = new List<IPlayerTargetsSelected>();

        List<IBattleMeterTick> IEventBroker<IBattleMeterTick>.Subscribers => _battleMeterSubscribers;
        List<IPlayerActionSelected> IEventBroker<IPlayerActionSelected>.Subscribers => _playerActionSelectedSubscribers;
        List<IPlayerTargetsSelected> IEventBroker<IPlayerTargetsSelected>.Subscribers => _playerTargetsSelectedSubscriber;

        public void Subscribe(IPlayerTargetsSelected subscriber) =>
            _playerTargetsSelectedSubscriber.Add(subscriber);
        public void Unsubscribe(IPlayerTargetsSelected subscriber) =>
            _playerTargetsSelectedSubscriber.Remove(subscriber);
        public void NotifyPlayerTargetsSelected(List<FighterController> targets) =>
            _playerTargetsSelectedSubscriber.ForEach(sub => sub.NotifyPlayerTargetsSelected(targets));

        public void Subscribe(IPlayerActionSelected subscriber) =>
            _playerActionSelectedSubscribers.Add(subscriber);
        public void Unsubscribe(IPlayerActionSelected subscriber) =>
            _playerActionSelectedSubscribers.Remove(subscriber);
        public void NotifyPlayerActionSelected(ATBFighterAction_SO action) =>
            _playerActionSelectedSubscribers.ForEach(sub => sub.NotifyPlayerActionSelected(action));

        public void Subscribe(IBattleMeterTick subscriber) =>
            _battleMeterSubscribers.Add(subscriber);
        public void Unsubscribe(IBattleMeterTick subscriber) =>
            _battleMeterSubscribers.Remove(subscriber);
        public void NotifyBattleMeterTick(FighterController fighter) =>
            _battleMeterSubscribers.ForEach(sub => sub.NotifyBattleMeterTick(fighter));

        private void SubscribeToEvents()
        {
            PlayerActions.OnPlayerActionButtonClick += NotifyPlayerActionSelected;
            PlayerTargets.OnPlayerTargetButtonClick += NotifyPlayerTargetsSelected;
            BattleState.OnBattleMeterTick += NotifyBattleMeterTick;
        }

        private void UnsubscribeToEvents()
        {
            PlayerActions.OnPlayerActionButtonClick -= NotifyPlayerActionSelected;
            PlayerTargets.OnPlayerTargetButtonClick -= NotifyPlayerTargetsSelected;
            BattleState.OnBattleMeterTick -= NotifyBattleMeterTick;
        }

        private void Start() => SubscribeToEvents();
        private void OnDestroy() => UnsubscribeToEvents();
    }
}