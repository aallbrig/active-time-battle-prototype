using System;
using Controllers;
using Data;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class PlayerActions : DynamicButtonContainer<FighterAction, Button>
    {
        public static event Action<FighterAction> OnPlayerActionButtonClick;

        public Button playerActionPrefab;

        public void SetupActions(FighterController fighter)
        {
            var actions = fighter.stats.actionSet.actions;
            SetupList(actions);
        }

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
