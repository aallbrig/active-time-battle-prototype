using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace GameEventSystem
{
    [CreateAssetMenu(fileName = "button clicked game event", menuName = "active time battle/events/button clicked game event")]
    public class ButtonClickedGameEvent : ScriptableObject
    {
        private readonly List<ButtonClickedGameEventListener> _listeners = new List<ButtonClickedGameEventListener>();
        public void Broadcast() => _listeners.ForEach(listener => listener.OnEventBroadcast());
        public void RegisterListener(ButtonClickedGameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(ButtonClickedGameEventListener listener) => _listeners.Remove(listener);
    }
}