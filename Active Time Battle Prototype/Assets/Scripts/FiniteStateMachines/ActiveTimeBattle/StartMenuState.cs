using System;
using Controllers;
using EventBroker;
using EventBroker.SubscriberInterfaces;
using Managers;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class StartMenuState : ActiveTimeBattleState
    {
        public StartMenuState(ActiveTimeBattleManager manager) : base(manager) {}

        public override void Enter()
        {
            Context.ToggleStartMenu(true);
            // Play start menu on enter animation
            // User is presented with start menu title
            // User is presented with start fight button
            // User is presented with quit game button
        }

        public override void Leave(Action callback)
        {
            Context.ToggleStartMenu(false);
            base.Leave(callback);
        }
    }
}