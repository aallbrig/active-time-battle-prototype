using System.Collections.Generic;
using Controllers;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ActionEffectDisplayTextManager : MonoBehaviour
    {
        public TextMeshProUGUI actionEffectDisplayText;
        public Transform targetCanvas;
        public Color healTextColor;
        public Color damageTextColor;
        public Vector3 offset = new Vector3(0, 1f, 0);
        private Camera _camera;

        public void DisplayHealText(FighterController fighter, float heal)
        {
            var text = Instantiate(actionEffectDisplayText, targetCanvas);
            var screenPoint = _camera.WorldToScreenPoint(fighter.transform.position + offset);
            text.transform.position = screenPoint;
            text.mesh.SetColors(new List<Color> { healTextColor });
            text.text = heal.ToString();
        }

        public void DisplayDamageText(FighterController fighter, float damage)
        {
            var text = Instantiate(actionEffectDisplayText, targetCanvas);
            var screenPoint = _camera.WorldToScreenPoint(fighter.transform.position + offset);
            text.transform.position = screenPoint;
            text.mesh.SetColors(new List<Color> { damageTextColor });
            text.text = damage.ToString();
        }

        private void Start()
        {
            _camera = Camera.main;
        }
    }
}