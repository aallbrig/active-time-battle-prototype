using System;
using GameEventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VictoryScreen : MonoBehaviour
    {
        public ButtonClickedGameEvent continueBattlingButtonClicked;
        public ButtonClickedGameEvent quitBattleButtonClicked;

        public Button continueBattlingButton;
        public Button quitBattleButton;

        private void EnableButtons()
        {
            continueBattlingButton.interactable = true;
            quitBattleButton.interactable = true;
        }

        private void DisableButtons()
        {
            continueBattlingButton.interactable = false;
            quitBattleButton.interactable = false;
        }

        private void OnEnable() => EnableButtons();

        private void Start()
        {
            continueBattlingButton.onClick.AddListener(() =>
            {
                DisableButtons();
                if (continueBattlingButtonClicked != null) continueBattlingButtonClicked.Broadcast();
            });
            quitBattleButton.onClick.AddListener(() =>
            {
                DisableButtons();
                if (quitBattleButtonClicked != null) quitBattleButtonClicked.Broadcast();
            });
        }
    }
}
