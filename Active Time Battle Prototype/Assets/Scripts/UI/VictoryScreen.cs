using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VictoryScreen : MonoBehaviour
    {
        public static event Action OnContinueBattlingButtonClick;
        public static event Action OnQuitButtonClick;

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
                OnContinueBattlingButtonClick?.Invoke();
            });
            quitBattleButton.onClick.AddListener(() =>
            {
                DisableButtons();
                OnQuitButtonClick?.Invoke();
            });
        }
    }
}
