using UnityEngine;
using UnityEngine.Events;

namespace GameEventSystem
{
    public class GameEventListener : MonoBehaviour 
    {
        public GameEvent Event;
        public UnityEvent onEventBroadcastHandlers;

        public void OnEventBroadcast() => onEventBroadcastHandlers.Invoke();

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
    }
}