using System;
using System.Collections.Generic;
using Controllers;
using Data;
using Managers;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class PlayerTargets : DynamicButtonContainer<FighterController, Button>
    {
        public static event Action<List<FighterController>> OnPlayerTargetButtonClick;

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
                OnPlayerTargetButtonClick?.Invoke(new List<FighterController> { element });
            });

            return button;
        }
    }
}
