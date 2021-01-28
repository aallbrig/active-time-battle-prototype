using Controllers;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ActionEffectDisplayTextManager : MonoBehaviour
    {
        public TextMeshProUGUI actionEffectDisplayText;
        public Transform targetCanvas;
        public Color healTextColor = Color.green;
        public Color damageTextColor = Color.red;
        public Vector3 offset = new Vector3(0, 1f, 0);
        private Camera _camera;

        public void DisplayHealText(FighterController fighter, float heal)
        {
            var text = Instantiate(actionEffectDisplayText, targetCanvas);
            var screenPoint = _camera.WorldToScreenPoint(fighter.transform.position + offset);
            text.transform.position = screenPoint;
            text.text = ((int) heal).ToString();
            text.color = healTextColor;
        }

        public void DisplayDamageText(FighterController fighter, float damage)
        {
            var text = Instantiate(actionEffectDisplayText, targetCanvas);
            var screenPoint = _camera.WorldToScreenPoint(fighter.transform.position + offset);
            text.transform.position = screenPoint;
            text.text = ((int) damage).ToString();
            text.color = damageTextColor;
        }

        private void Start()
        {
            _camera = Camera.main;
        }
    }
}