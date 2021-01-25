using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonUiContainer<ListItemType, ButtonType> : MonoBehaviour where ButtonType : Button
    {
        public Transform containerTransform;

        protected List<ButtonType> Buttons = new List<ButtonType>();
        protected List<ListItemType> _list = new List<ListItemType>();

        protected abstract ButtonType GenerateUiElement(ListItemType element);

        private void ClearContainer()
        {
            foreach (Transform child in containerTransform.transform)
                Destroy(child.gameObject);
            Buttons.ForEach(Destroy);
            Buttons.Clear();
        }

        protected void SetupList(List<ListItemType> list)
        {
            ClearContainer();
            _list = list;
            Buttons = GenerateContainerUiElements(_list);
        }

        private List<ButtonType> GenerateContainerUiElements(List<ListItemType> list) => list.Select(GenerateUiElement).ToList();

        public void DisableButtons() => Buttons.ForEach(button => button.interactable = false);
        public void EnableButtons() => Buttons.ForEach(button => button.interactable = true);
    }
}