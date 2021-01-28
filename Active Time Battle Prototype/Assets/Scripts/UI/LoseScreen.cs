using GameEventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseScreen : MonoBehaviour
    {
        public ButtonClickedGameEvent restartButtonClicked;
        public Button restartButton;

        private void EnableButtons()
        {
            restartButton.interactable = true;
        }

        private void DisableButtons()
        {
            restartButton.interactable = false;
        }

        private void OnEnable() => EnableButtons();

        private void Start()
        {
            restartButton.onClick.AddListener(() =>
            {
                DisableButtons();
                if (restartButtonClicked != null) restartButtonClicked.Broadcast();
            });
        }
    }
}
