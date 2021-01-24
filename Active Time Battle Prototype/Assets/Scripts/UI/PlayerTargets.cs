﻿using System;
using System.Collections.Generic;
using ATBFighter;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class PlayerTargets : ButtonUiContainer<FighterController, Button>
    {
        public static event Action<List<FighterController>> OnPlayerTargetButtonClick;
        public Button playerTargetButtonPrefab;

        // alias method so my mind doesn't bend/break on generic name
        public void SetTargets(List<FighterController> targets) => SetupList(targets);
        public void SelectAllTargets() => OnPlayerTargetButtonClick?.Invoke(_list);

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
