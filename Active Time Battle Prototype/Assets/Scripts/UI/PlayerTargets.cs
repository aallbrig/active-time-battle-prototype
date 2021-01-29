using System;
using System.Collections.Generic;
using Controllers;
using Data;
using GameEventSystem;
using Managers;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class PlayerTargets : DynamicButtonContainer<FighterController, Button>
    {
        public FighterTargetsGameEvent playerFighterTargetsSelected;
        public Button playerTargetButtonPrefab;

        public void Render(List<FighterController> targets) => SetupList(targets);

        protected override Button GenerateUiElement(FighterController element)
        {
            var button = Instantiate(playerTargetButtonPrefab, containerTransform);
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = element.stats.fighterName;
            button.onClick.AddListener(() =>
            {
                DisableButtons();
                // TODO: get multiple target selection working
                var targets = new List<FighterController> {element};
                if (playerFighterTargetsSelected != null) playerFighterTargetsSelected.Broadcast(targets);
            });

            return button;
        }
    }
}
