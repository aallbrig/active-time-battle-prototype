using System.Collections.Generic;
using Controllers;
using GameEventSystem;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class PlayerTargets : DynamicButtonContainer<FighterController, Button>
    {
        public FighterGameEvent onTargetHover;
        public FighterTargetsGameEvent playerFighterTargetsSelected;
        public Button playerTargetButtonPrefab;

        public void Render(List<FighterController> targets) => SetupList(targets);

        protected override Button GenerateUiElement(FighterController element)
        {
            var button = Instantiate(playerTargetButtonPrefab, containerTransform);
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = element.stats.fighterName;
            // I can't believe I can't just add button.onHover.AddListener!
            var eventEntry = new EventTrigger.Entry();
            eventEntry.eventID = EventTriggerType.PointerEnter;
            eventEntry.callback.AddListener((eventData) =>
            {
                if (onTargetHover != null) onTargetHover.Broadcast(element);
            });
            button.gameObject.AddComponent<EventTrigger>();
            button.gameObject.GetComponent<EventTrigger>().triggers.Add(eventEntry);

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
