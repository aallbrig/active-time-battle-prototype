using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerFighterStats : MonoBehaviour
    {
        public TextMeshProUGUI fighterName;
        public TextMeshProUGUI fighterHealth;
        public Slider battleMeter;
        public GameObject highlight;

        private FighterController _fighter;

        public void SetFighter(FighterController fighter)
        {
            _fighter = fighter;

            UpdateUiElements();
        }

        private void UpdateUiElements()
        {
            // TODO: Name, max health values aren't going to change a bunch...
            fighterName.text = _fighter.stats.fighterName;

            var currentHealth = ((int) _fighter.stats.currentHealth).ToString();
            var maxHealth = ((int) _fighter.stats.maxHealth).ToString();
            fighterHealth.text = currentHealth + " / " + maxHealth;

            battleMeter.value = _fighter.stats.currentBattleMeterValue;
        }

        public void UpdateIfTargeted(FighterController attacker, FighterAction action, List<FighterController> targets)
        {
            if (targets.Contains(_fighter)) UpdateUiElements();
        }

        public void UpdateOnBattleMeterTicker(FighterController fighter)
        {
            if (fighter == _fighter) UpdateUiElements();
        }

        public void ActivePlayerSet(FighterController fighter) => highlight.SetActive(fighter == _fighter);

        public void PlayerTargetsSelected(List<FighterController> targets) => highlight.SetActive(false);
    }
}
