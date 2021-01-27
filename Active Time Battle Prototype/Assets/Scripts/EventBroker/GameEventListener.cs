using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventBroker
{
    // Implementation of event broker except heavily leveraging scriptable objects
    // TODO: Fully replace EventBroker with this implementation, if it proves very useful

    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent response;
        public void OnEventRaised() => response.Invoke();

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
    }
}