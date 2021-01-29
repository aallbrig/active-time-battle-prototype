using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace GameEventSystem
{
    [CreateAssetMenu(fileName = "fighter targets game event", menuName = "active time battle/events/fighter targets game event")]
    public class FighterTargetsGameEvent : ScriptableObject
    {
        private readonly List<FighterTargetsGameEventListener> _listeners = new List<FighterTargetsGameEventListener>();
        public void Broadcast(List<FighterController> targets) => _listeners.ForEach(listener => listener.OnEventBroadcast(targets));
        public void RegisterListener(FighterTargetsGameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(FighterTargetsGameEventListener listener) => _listeners.Remove(listener);
    }
}