using System;
using Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace GameEventSystem
{
    // If you wish to use a generic UnityEvent type you must override the class type
    // https://docs.unity3d.com/ScriptReference/Events.UnityEvent_1.html
    [Serializable]
    public class FighterGameEventUnityEvent : UnityEvent<FighterController> { }

    public class FighterGameEventListener : MonoBehaviour 
    {
        public FighterGameEvent Event;
        public FighterGameEventUnityEvent onEventRaisedHandlers;

        public void OnEventBroadcast(FighterController fighter) => onEventRaisedHandlers.Invoke(fighter);

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
    }
}