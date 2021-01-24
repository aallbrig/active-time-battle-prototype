using System;
using System.Collections.Generic;
using ATBFighter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerActions : MonoBehaviour
    {
        public static event Action<ATBFighterAction_SO> OnPlayerActionButtonClick;

        public Transform playerActionsContainerUi;
        public Button playerActionPrefab;
        private List<ATBFighterAction_SO> _actions = new List<ATBFighterAction_SO>();
        private List<Button> _buttons = new List<Button>();

        public void SetActions(List<ATBFighterAction_SO> actions)
        {
            ResetActionButtonSpace();
            _actions = actions;
            GenerateActionsUi();
        }

        private void ResetActionButtonSpace()
        {
            foreach (Transform child in playerActionsContainerUi.transform)
                Destroy(child.gameObject);
            _buttons.ForEach(Destroy);
            _buttons.Clear();
            _actions.Clear();
        }

        private void GenerateActionButtonUi(ATBFighterAction_SO action)
        {
            var button = Instantiate(playerActionPrefab, playerActionsContainerUi);
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = action.actionName;
            button.onClick.AddListener(() =>
            {
                DisableActionButtons();
                OnPlayerActionButtonClick?.Invoke(action);
            });

            _buttons.Add(button);
        }

        private void GenerateActionsUi() => _actions.ForEach(GenerateActionButtonUi);
        private void DisableActionButtons() => _buttons.ForEach(button => button.interactable = false);
        private void EnableActionButtons() => _buttons.ForEach(button => button.interactable = true);
    }
}
