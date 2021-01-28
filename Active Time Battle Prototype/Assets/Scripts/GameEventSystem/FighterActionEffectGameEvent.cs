using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace GameEventSystem
{
    [CreateAssetMenu(fileName = "fighter action effect game event", menuName = "active time battle/events/fighter action effect game event")]
    public class FighterActionEffectGameEvent : ScriptableObject
    {
        private readonly List<FighterActionEffectGameEventListener> _listeners = new List<FighterActionEffectGameEventListener>();
        public void Broadcast(FighterController fighter, float actionEffect) =>
            _listeners.ForEach(listener => listener.OnEventBroadcast(fighter, actionEffect));
        public void RegisterListener(FighterActionEffectGameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(FighterActionEffectGameEventListener listener) => _listeners.Remove(listener);
    }
}