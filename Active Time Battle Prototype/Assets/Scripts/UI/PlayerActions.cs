using System;
using Controllers;
using GameEventSystem;
using ScriptableObjects;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class PlayerActions : DynamicButtonContainer<FighterAction, Button>
    {
        public FighterActionGameEvent playerFighterActionSelect;
        public Button playerActionPrefab;

        public void SetupActions(FighterController fighter)
        {
            var actions = fighter.stats.actionSet.actions;
            SetupList(actions);
        }

        protected override Button GenerateUiElement(FighterAction element)
        {
            var action = element;
            var button = Instantiate(playerActionPrefab, containerTransform);
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = element.actionName;
            button.onClick.AddListener(() =>
            {
                if (playerFighterActionSelect != null) playerFighterActionSelect.Broadcast(action);
                DisableButtons();
            });

            return button;
        }
    }
}
