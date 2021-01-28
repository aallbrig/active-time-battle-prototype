using System;
using Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace GameEventSystem
{
    // If you wish to use a generic UnityEvent type you must override the class type
    // https://docs.unity3d.com/ScriptReference/Events.UnityEvent_1.html
    [Serializable]
    public class FighterActionEffectGameEventUnityEvent : UnityEvent<FighterController, float> { }

    public class FighterActionEffectGameEventListener : MonoBehaviour 
    {
        public FighterActionEffectGameEvent Event;
        public FighterActionEffectGameEventUnityEvent onEventRaisedHandlers;

        public void OnEventBroadcast(FighterController fighter, float actionEffect) =>
            onEventRaisedHandlers.Invoke(fighter, actionEffect);

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
    }
}