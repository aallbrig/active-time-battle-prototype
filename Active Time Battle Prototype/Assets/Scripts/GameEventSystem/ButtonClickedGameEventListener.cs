using UnityEngine;
using UnityEngine.Events;

namespace GameEventSystem
{
    public class ButtonClickedGameEventListener : MonoBehaviour 
    {
        public ButtonClickedGameEvent Event;
        public UnityEvent onEventBroadcastHandlers;

        public void OnEventBroadcast() => onEventBroadcastHandlers.Invoke();

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
    }
}