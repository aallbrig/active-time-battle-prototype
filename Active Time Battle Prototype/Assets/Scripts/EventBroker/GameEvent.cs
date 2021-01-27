using System.Collections.Generic;
using UnityEngine;

namespace EventBroker
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "active time battle/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

        public void Raise() => _listeners.ForEach(listener => listener.OnEventRaised());
        public void RegisterListener(GameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(GameEventListener listener) => _listeners.Remove(listener);
    }
}