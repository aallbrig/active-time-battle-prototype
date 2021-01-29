using System.Collections.Generic;
using Controllers;
using Data;
using ScriptableObjects;
using UnityEngine;

namespace GameEventSystem
{
    [CreateAssetMenu(fileName = "fighter action execute game event", menuName = "active time battle/events/fighter action execute game event")]
    public class FighterActionExecuteGameEvent : ScriptableObject
    {
        private readonly List<FighterActionExecuteGameEventListener> _listeners = new List<FighterActionExecuteGameEventListener>();
        public void Broadcast(FighterController fighter, FighterAction action, List<FighterController> targets) =>
            _listeners.ForEach(listener => listener.OnEventBroadcast(fighter, action, targets));
        public void RegisterListener(FighterActionExecuteGameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(FighterActionExecuteGameEventListener listener) => _listeners.Remove(listener);
    }
}