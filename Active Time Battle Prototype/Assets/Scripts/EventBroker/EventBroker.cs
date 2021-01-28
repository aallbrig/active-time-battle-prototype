using System.Collections.Generic;
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
        IEventBroker<IPlayerFighterCreated>, IPlayerFighterCreated,
        IEventBroker<IEnemyFighterCreated>, IEnemyFighterCreated,
        IEventBroker<IContinueBattlingButtonClicked>, IContinueBattlingButtonClicked,
        IEventBroker<IRestartButtonClicked>, IRestartButtonClicked,
        IEventBroker<IQuitButtonClicked>, IQuitButtonClicked,
        IEventBroker<IFighterAction>, IFighterAction,
        IEventBroker<IFighterTakeDamage>, IFighterTakeDamage,
        IEventBroker<IFighterHeal>, IFighterHeal,
        IEventBroker<IFighterDie>, IFighterDie,
        IEventBroker<IFighterActionEnqueueRequest>, IFighterActionEnqueueRequest
    {
        public static EventBroker Instance { get; private set; }

        #region Subscriber lists

        private readonly List<IPlayerActionSelected> _playerActionSelectedSubscribers = new List<IPlayerActionSelected>();
        private readonly List<IPlayerTargetsSelected> _playerTargetsSelectedSubscribers = new List<IPlayerTargetsSelected>();
        private readonly List<IPlayerFighterCreated> _playerFighterCreatedSubscribers = new List<IPlayerFighterCreated>();
        private readonly List<IEnemyFighterCreated> _enemyFighterCreatedSubscribers = new List<IEnemyFighterCreated>();
        private readonly List<IStartBattleButtonClicked> _startBattleButtonSubscribers = new List<IStartBattleButtonClicked>();
        private readonly List<IContinueBattlingButtonClicked> _continueBattlingButtonClickedSubscribers = new List<IContinueBattlingButtonClicked>();
        private readonly List<IRestartButtonClicked> _restartButtonClickedSubscribers = new List<IRestartButtonClicked>();
        private readonly List<IQuitButtonClicked> _quitButtonClickedSubscribers = new List<IQuitButtonClicked>();
        private readonly List<IFighterAction> _fighterActionSubscribers = new List<IFighterAction>();
        private readonly List<IFighterTakeDamage> _fighterTakeDamageSubscribers = new List<IFighterTakeDamage>();
        private readonly List<IFighterHeal> _fighterHealSubscribers = new List<IFighterHeal>();
        private readonly List<IFighterDie> _fighterDieSubscribers = new List<IFighterDie>();
        private readonly List<IFighterActionEnqueueRequest> _fighterCommandSubscribers = new List<IFighterActionEnqueueRequest>();


        List<IPlayerActionSelected> IEventBroker<IPlayerActionSelected>.Subscribers => _playerActionSelectedSubscribers;
        List<IPlayerTargetsSelected> IEventBroker<IPlayerTargetsSelected>.Subscribers => _playerTargetsSelectedSubscribers;
        List<IPlayerFighterCreated> IEventBroker<IPlayerFighterCreated>.Subscribers => _playerFighterCreatedSubscribers;
        List<IEnemyFighterCreated> IEventBroker<IEnemyFighterCreated>.Subscribers => _enemyFighterCreatedSubscribers;
        List<IContinueBattlingButtonClicked> IEventBroker<IContinueBattlingButtonClicked>.Subscribers => _continueBattlingButtonClickedSubscribers;
        List<IRestartButtonClicked> IEventBroker<IRestartButtonClicked>.Subscribers => _restartButtonClickedSubscribers;
        List<IQuitButtonClicked> IEventBroker<IQuitButtonClicked>.Subscribers => _quitButtonClickedSubscribers;
        List<IFighterAction> IEventBroker<IFighterAction>.Subscribers => _fighterActionSubscribers;
        List<IFighterTakeDamage> IEventBroker<IFighterTakeDamage>.Subscribers => _fighterTakeDamageSubscribers;
        List<IFighterHeal> IEventBroker<IFighterHeal>.Subscribers => _fighterHealSubscribers;
        List<IFighterDie> IEventBroker<IFighterDie>.Subscribers => _fighterDieSubscribers;
        List<IFighterActionEnqueueRequest> IEventBroker<IFighterActionEnqueueRequest>.Subscribers =>
            _fighterCommandSubscribers;

        #endregion


        public void Subscribe(IFighterActionEnqueueRequest subscriber) =>
            _fighterCommandSubscribers.Add(subscriber);
        public void Unsubscribe(IFighterActionEnqueueRequest subscriber) =>
            _fighterCommandSubscribers.Remove(subscriber);
        public void NotifyFighterCommand(ICommand fighterCommand) =>
            _fighterCommandSubscribers.ForEach(sub => sub.NotifyFighterCommand(fighterCommand));

        public void Subscribe(IFighterDie subscriber) => _fighterDieSubscribers.Add(subscriber);
        public void Unsubscribe(IFighterDie subscriber) => _fighterDieSubscribers.Remove(subscriber);
        public void NotifyFighterDie(FighterController fighter) =>
            _fighterDieSubscribers.ForEach(sub => sub.NotifyFighterDie(fighter));

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

        public void Subscribe(IQuitButtonClicked subscriber) => _quitButtonClickedSubscribers.Add(subscriber);
        public void Unsubscribe(IQuitButtonClicked subscriber) => _quitButtonClickedSubscribers.Remove(subscriber);
        public void NotifyQuitButtonClicked() => _quitButtonClickedSubscribers.ForEach(sub => sub.NotifyQuitButtonClicked());

        public void Subscribe(IRestartButtonClicked subscriber) => _restartButtonClickedSubscribers.Add(subscriber);
        public void Unsubscribe(IRestartButtonClicked subscriber) => _restartButtonClickedSubscribers.Remove(subscriber);
        public void NotifyRestartButtonClicked() => _restartButtonClickedSubscribers.ForEach(sub => sub.NotifyRestartButtonClicked());

        public void Subscribe(IContinueBattlingButtonClicked subscriber) =>
            _continueBattlingButtonClickedSubscribers.Add(subscriber);
        public void Unsubscribe(IContinueBattlingButtonClicked subscriber) =>
            _continueBattlingButtonClickedSubscribers.Remove(subscriber);
        public void NotifyContinueBattlingButtonClick() =>
            _continueBattlingButtonClickedSubscribers.ForEach(sub => sub.NotifyContinueBattlingButtonClick());

        public void Subscribe(IEnemyFighterCreated subscriber) =>
            _enemyFighterCreatedSubscribers.Add(subscriber);
        public void Unsubscribe(IEnemyFighterCreated subscriber) =>
            _enemyFighterCreatedSubscribers.Remove(subscriber);
        public void NotifyEnemyFighterCreated(FighterController fighter) =>
            _enemyFighterCreatedSubscribers.ForEach(sub => sub.NotifyEnemyFighterCreated(fighter));

        public void Subscribe(IPlayerFighterCreated subscriber) =>
            _playerFighterCreatedSubscribers.Add(subscriber);
        public void Unsubscribe(IPlayerFighterCreated subscriber) =>
            _playerFighterCreatedSubscribers.Remove(subscriber);
        public void NotifyPlayerFighterCreated(FighterController fighter) =>
            _playerFighterCreatedSubscribers.ForEach(sub => sub.NotifyPlayerFighterCreated(fighter));

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
            ActiveTimeBattleManager.OnEnemyFighterCreated += NotifyEnemyFighterCreated;
            ActiveTimeBattleManager.OnPlayerFighterCreated += NotifyPlayerFighterCreated;
            VictoryScreen.OnContinueBattlingButtonClick += NotifyContinueBattlingButtonClick;
            VictoryScreen.OnQuitButtonClick += NotifyQuitButtonClicked;
            LoseScreen.OnRestartButtonClick += NotifyQuitButtonClicked;
            FighterController.OnFighterAction += NotifyFighterAction;
            FighterController.OnFighterTakeDamage += NotifyFighterTakeDamage;
            FighterController.OnFighterHeal += NotifyFighterHeal;
            FighterController.OnFighterDie += NotifyFighterDie;
            PlayerInputManager.OnPlayerFighterCommand += NotifyFighterCommand;
            EnemyAIManager.OnEnemyAiFighterCommand += NotifyFighterCommand;
        }

        private void UnsubscribeToEvents()
        {
            PlayerActions.OnPlayerActionButtonClick -= NotifyPlayerActionSelected;
            PlayerTargets.OnPlayerTargetButtonClick -= NotifyPlayerTargetsSelected;
            ActiveTimeBattleManager.OnEnemyFighterCreated -= NotifyEnemyFighterCreated;
            ActiveTimeBattleManager.OnPlayerFighterCreated -= NotifyPlayerFighterCreated;
            VictoryScreen.OnContinueBattlingButtonClick -= NotifyContinueBattlingButtonClick;
            VictoryScreen.OnQuitButtonClick -= NotifyQuitButtonClicked;
            LoseScreen.OnRestartButtonClick -= NotifyQuitButtonClicked;
            FighterController.OnFighterAction -= NotifyFighterAction;
            FighterController.OnFighterTakeDamage -= NotifyFighterTakeDamage;
            FighterController.OnFighterHeal -= NotifyFighterHeal;
            FighterController.OnFighterDie -= NotifyFighterDie;
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