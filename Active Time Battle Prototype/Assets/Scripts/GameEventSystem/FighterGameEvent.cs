using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace GameEventSystem
{
    [CreateAssetMenu(fileName = "fighter game event", menuName = "active time battle/events/fighter game event")]
    public class FighterGameEvent : ScriptableObject
    {
        public string description = "Default description of event involving a fighter";

        private readonly List<FighterGameEventListener> _listeners = new List<FighterGameEventListener>();
        public void Broadcast(FighterController fighter) => _listeners.ForEach(listener => listener.OnEventBroadcast(fighter));
        public void RegisterListener(FighterGameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(FighterGameEventListener listener) => _listeners.Remove(listener);
    }
}