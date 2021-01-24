using System;
using System.Collections.Generic;
using ATBFighter;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class PlayerActions : ButtonUiContainer<ATBFighterAction_SO, Button>
    {
        public static event Action<ATBFighterAction_SO> OnPlayerActionButtonClick;

        public Button playerActionPrefab;

        // Alias, so my mind doesn't bend/break on generic naming
        public void SetActions(List<ATBFighterAction_SO> actions) => SetupList(actions);

        protected override Button GenerateUiElement(ATBFighterAction_SO element)
        {
            var button = Instantiate(playerActionPrefab, containerTransform);
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = element.actionName;
            button.onClick.AddListener(() =>
            {
                DisableButtons();
                OnPlayerActionButtonClick?.Invoke(element);
            });

            return button;
        }
    }
}
