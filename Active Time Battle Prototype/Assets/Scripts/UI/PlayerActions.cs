using System;
using System.Collections.Generic;
using Data;
using Data.Actions;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class PlayerActions : DynamicButtonContainer<FighterAction, Button>
    {
        public static event Action<FighterAction> OnPlayerActionButtonClick;

        public Button playerActionPrefab;

        // Alias, so my mind doesn't bend/break on generic naming
        public void SetActions(List<FighterAction> actions) => SetupList(actions);

        private FighterAction _action;

        protected override Button GenerateUiElement(FighterAction element)
        {
            var action = element;
            var button = Instantiate(playerActionPrefab, containerTransform);
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = element.actionName;
            button.onClick.AddListener(() =>
            {
                OnPlayerActionButtonClick?.Invoke(action);
                DisableButtons();
            });

            return button;
        }
    }
}
