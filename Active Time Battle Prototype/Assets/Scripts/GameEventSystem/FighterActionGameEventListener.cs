using System;
using System.Collections.Generic;
using Controllers;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace GameEventSystem
{
    // If you wish to use a generic UnityEvent type you must override the class type
    // https://docs.unity3d.com/ScriptReference/Events.UnityEvent_1.html
    [Serializable]
    public class FighterActionGameEventUnityEvent : UnityEvent<FighterController, FighterAction, List<FighterController>> { }

    public class FighterActionGameEventListener : MonoBehaviour 
    {
        public FighterActionGameEvent Event;
        public FighterActionGameEventUnityEvent onEventRaisedHandlers;

        public void OnEventBroadcast(FighterController fighter, FighterAction action, List<FighterController> targets) =>
            onEventRaisedHandlers.Invoke(fighter, action, targets);

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
    }
}