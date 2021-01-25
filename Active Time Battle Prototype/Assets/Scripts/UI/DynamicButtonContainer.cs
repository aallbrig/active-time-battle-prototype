using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class DynamicButtonContainer<ListItemType, ButtonType> : MonoBehaviour where ButtonType : Button
    {
        public Transform containerTransform;

        private List<ButtonType> _buttons = new List<ButtonType>();

        protected abstract ButtonType GenerateUiElement(ListItemType element);

        private void ClearContainer()
        {
            foreach (Transform child in containerTransform.transform)
                Destroy(child.gameObject);
        }

        protected void SetupList(List<ListItemType> list)
        {
            ClearContainer();
            _buttons = GenerateContainerUiElements(list);
        }

        private List<ButtonType> GenerateContainerUiElements(List<ListItemType> list) => list.Select(GenerateUiElement).ToList();

        public void DisableButtons() => _buttons.ForEach(button => button.interactable = false);
        public void EnableButtons() => _buttons.ForEach(button => button.interactable = true);
    }
}