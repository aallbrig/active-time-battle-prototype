using System.Collections.Generic;
using Controllers;
using Data.Actions;
using FiniteStateMachines.ActiveTimeBattle;
using UI;
using UnityEngine;

namespace EventBroker
{
    public class EventBroker : MonoBehaviour,
        IEventBroker<IBattleMeterTick>, IBattleMeterTick,
        IEventBroker<IPlayerActionSelected>, IPlayerActionSelected,
        IEventBroker<IPlayerTargetsSelected>, IPlayerTargetsSelected,
        IEventBroker<IPlayerFighterCreated>, IPlayerFighterCreated,
        IEventBroker<IEnemyFighterCreated>, IEnemyFighterCreated,
        IEventBroker<IStartBattleButtonClicked>, IStartBattleButtonClicked,
        IEventBroker<IContinueBattlingButtonClicked>, IContinueBattlingButtonClicked,
        IEventBroker<IRestartButtonClicked>, IRestartButtonClicked,
        IEventBroker<IQuitButtonClicked>, IQuitButtonClicked
    {
        public static EventBroker Instance { get; private set; }

        #region Subscriber lists

        private readonly List<IBattleMeterTick> _battleMeterSubscribers = new List<IBattleMeterTick>();
        private readonly List<IPlayerActionSelected> _playerActionSelectedSubscribers = new List<IPlayerActionSelected>();
        private readonly List<IPlayerTargetsSelected> _playerTargetsSelectedSubscribers = new List<IPlayerTargetsSelected>();
        private readonly List<IPlayerFighterCreated> _playerFighterCreatedSubscribers = new List<IPlayerFighterCreated>();
        private readonly List<IEnemyFighterCreated> _enemyFighterCreatedSubscribers = new List<IEnemyFighterCreated>();
        private readonly List<IStartBattleButtonClicked> _startBattleButtonSubscribers = new List<IStartBattleButtonClicked>();
        private readonly List<IContinueBattlingButtonClicked> _continueBattlingButtonClickedSubscribers = new List<IContinueBattlingButtonClicked>();
        private readonly List<IRestartButtonClicked> _restartButtonClickedSubscribers = new List<IRestartButtonClicked>();
        private readonly List<IQuitButtonClicked> _quitButtonClickedSubscribers = new List<IQuitButtonClicked>();


        List<IPlayerActionSelected> IEventBroker<IPlayerActionSelected>.Subscribers => _playerActionSelectedSubscribers;
        List<IPlayerTargetsSelected> IEventBroker<IPlayerTargetsSelected>.Subscribers => _playerTargetsSelectedSubscribers;
        List<IPlayerFighterCreated> IEventBroker<IPlayerFighterCreated>.Subscribers => _playerFighterCreatedSubscribers;
        List<IEnemyFighterCreated> IEventBroker<IEnemyFighterCreated>.Subscribers => _enemyFighterCreatedSubscribers;
        List<IStartBattleButtonClicked> IEventBroker<IStartBattleButtonClicked>.Subscribers => _startBattleButtonSubscribers;
        List<IContinueBattlingButtonClicked> IEventBroker<IContinueBattlingButtonClicked>.Subscribers => _continueBattlingButtonClickedSubscribers;
        List<IBattleMeterTick> IEventBroker<IBattleMeterTick>.Subscribers => _battleMeterSubscribers;
        List<IRestartButtonClicked> IEventBroker<IRestartButtonClicked>.Subscribers => _restartButtonClickedSubscribers;
        List<IQuitButtonClicked> IEventBroker<IQuitButtonClicked>.Subscribers => _quitButtonClickedSubscribers;

        #endregion

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

        public void Subscribe(IStartBattleButtonClicked subscriber) => _startBattleButtonSubscribers.Add(subscriber);
        public void Unsubscribe(IStartBattleButtonClicked subscriber) => _startBattleButtonSubscribers.Remove(subscriber);
        public void NotifyStartBattleButtonClicked() => _startBattleButtonSubscribers.ForEach(sub => sub.NotifyStartBattleButtonClicked());


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
            StartMenuState.OnPlayerFighterCreated += NotifyPlayerFighterCreated;
            StartMenu.OnStartBattleButtonClicked += NotifyStartBattleButtonClicked;
            BeginBattleState.OnEnemyFighterCreated += NotifyEnemyFighterCreated;
            VictoryScreen.OnContinueBattlingButtonClick += NotifyContinueBattlingButtonClick;
            VictoryScreen.OnQuitButtonClick += NotifyQuitButtonClicked;
            LoseScreen.OnRestartButtonClick += NotifyQuitButtonClicked;
        }

        private void UnsubscribeToEvents()
        {
            PlayerActions.OnPlayerActionButtonClick -= NotifyPlayerActionSelected;
            PlayerTargets.OnPlayerTargetButtonClick -= NotifyPlayerTargetsSelected;
            BattleState.OnBattleMeterTick -= NotifyBattleMeterTick;
            StartMenuState.OnPlayerFighterCreated -= NotifyPlayerFighterCreated;
            StartMenu.OnStartBattleButtonClicked -= NotifyStartBattleButtonClicked;
            BeginBattleState.OnEnemyFighterCreated -= NotifyEnemyFighterCreated;
            VictoryScreen.OnContinueBattlingButtonClick -= NotifyContinueBattlingButtonClick;
            VictoryScreen.OnQuitButtonClick -= NotifyQuitButtonClicked;
            LoseScreen.OnRestartButtonClick -= NotifyQuitButtonClicked;
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