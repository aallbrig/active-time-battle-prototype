using System;
using System.Collections.Generic;
using ATBFighter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class FighterTester : MonoBehaviour
    {
        public FighterController fighter;
        public List<FighterController> targets;
        public Transform buttonContainer;
        public Button buttonPrefab;

        public void OnEnable()
        {
            var actions = fighter.GetActions();

            actions.ForEach((action) =>
            {
                var button = Instantiate(buttonPrefab, buttonContainer);
                var text = button.GetComponentInChildren<TextMeshProUGUI>();
                text.text = action.actionName;
                button.onClick.AddListener(() =>
                {
                    fighter.ExecuteAction(action, targets);
                });
            });
        }

        public void OnDisable()
        {
            foreach (Transform child in buttonContainer.transform)
                Destroy(child);
        }
    }
}