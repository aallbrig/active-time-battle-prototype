using GameEventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        public ButtonClickedGameEvent buttonClicked;
        public Button startBattlingButton;

        private void OnEnable()
        {
            startBattlingButton.interactable = true;
        }

        private void Start()
        {
            startBattlingButton.onClick.AddListener(() =>
            {
                startBattlingButton.interactable = false;
                if (buttonClicked != null) buttonClicked.Broadcast();
            });
        }
    }
}
