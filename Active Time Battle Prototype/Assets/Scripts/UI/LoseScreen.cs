using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseScreen : MonoBehaviour
    {
        public static event Action OnRestartButtonClick;

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
                OnRestartButtonClick?.Invoke();
            });
        }
    }
}
