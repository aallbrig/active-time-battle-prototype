using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace GameEventSystem
{
    [CreateAssetMenu(fileName = "fighter action game event", menuName = "active time battle/events/fighter action game event")]
    public class FighterActionGameEvent : ScriptableObject
    {
        private readonly List<FighterActionGameEventListener> _listeners = new List<FighterActionGameEventListener>();
        public void Broadcast(FighterAction action) => _listeners.ForEach(listener => listener.OnEventBroadcast(action));
        public void RegisterListener(FighterActionGameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(FighterActionGameEventListener listener) => _listeners.Remove(listener);
    }
}