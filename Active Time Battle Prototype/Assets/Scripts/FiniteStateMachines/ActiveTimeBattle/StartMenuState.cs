using System;
using Controllers;
using EventBroker;
using EventBroker.SubscriberInterfaces;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FiniteStateMachines.ActiveTimeBattle
{
    public class StartMenuState : ActiveTimeBattleState
    {
        public StartMenuState(ActiveTimeBattleController controller) : base(controller) {}

        public override void Enter()
        {
            Controller.ToggleStartMenu(true);
            // Play start menu on enter animation
            // User is presented with start menu title
            // User is presented with start fight button
            // User is presented with quit game button
        }

        public override void Leave(Action callback)
        {
            Controller.ToggleStartMenu(false);
            base.Leave(callback);
        }
    }
}