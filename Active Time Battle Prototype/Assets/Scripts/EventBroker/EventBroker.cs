using System.Collections.Generic;
using Commands;
using EventBroker.SubscriberInterfaces;
using Managers;
using ScriptableObjects.FiniteStateMachines.PlayerInput.Actions;
using UnityEngine;

namespace EventBroker
{
    public class EventBroker : MonoBehaviour,
        IEventBroker<IFighterActionEnqueueRequest>, IFighterActionEnqueueRequest
    {
        public static EventBroker Instance { get; private set; }

        #region Subscriber lists

        private readonly List<IFighterActionEnqueueRequest> _fighterCommandSubscribers = new List<IFighterActionEnqueueRequest>();

        List<IFighterActionEnqueueRequest> IEventBroker<IFighterActionEnqueueRequest>.Subscribers =>
            _fighterCommandSubscribers;
        #endregion


        public void Subscribe(IFighterActionEnqueueRequest subscriber) =>
            _fighterCommandSubscribers.Add(subscriber);
        public void Unsubscribe(IFighterActionEnqueueRequest subscriber) =>
            _fighterCommandSubscribers.Remove(subscriber);
        public void NotifyFighterCommand(ICommand fighterCommand) =>
            _fighterCommandSubscribers.ForEach(sub => sub.NotifyFighterCommand(fighterCommand));

        private void SubscribeToEvents()
        {
            EnemyAIManager.OnEnemyAiFighterCommand += NotifyFighterCommand;
            PlayerInputEnqueueAction.OnPlayerFighterCommand += NotifyFighterCommand;
        }

        private void UnsubscribeToEvents()
        {
            EnemyAIManager.OnEnemyAiFighterCommand -= NotifyFighterCommand;
            PlayerInputEnqueueAction.OnPlayerFighterCommand -= NotifyFighterCommand;
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