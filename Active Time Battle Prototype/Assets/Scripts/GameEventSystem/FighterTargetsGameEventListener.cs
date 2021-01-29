using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace GameEventSystem
{
    // If you wish to use a generic UnityEvent type you must override the class type
    // https://docs.unity3d.com/ScriptReference/Events.UnityEvent_1.html
    [Serializable]
    public class FighterTargetsGameEventUnityEvent : UnityEvent<List<FighterController>> { }

    public class FighterTargetsGameEventListener : MonoBehaviour 
    {
        public FighterTargetsGameEvent Event;
        public FighterTargetsGameEventUnityEvent onEventRaisedHandlers;

        public void OnEventBroadcast(List<FighterController> targets) => onEventRaisedHandlers.Invoke(targets);

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
    }
}
