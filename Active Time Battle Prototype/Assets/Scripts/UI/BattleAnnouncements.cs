using TMPro;
using UnityEngine;

namespace UI
{
    public class BattleAnnouncements : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public void SetBattleAnnouncement(string message)
        {
            text.text = message;
        }

        public void ClearBattleAnnouncement()
        {
            text.text = "";
        }

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        private void OnEnable()
        {
            // Animate in
        }

        private void OnDisable()
        {
            // Animate out
        }
    }
}