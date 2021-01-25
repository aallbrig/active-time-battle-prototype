using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        public static event Action OnStartBattleButtonClicked;
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
                OnStartBattleButtonClicked?.Invoke();
            });
        }
    }
}
