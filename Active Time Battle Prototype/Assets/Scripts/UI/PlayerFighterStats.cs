using ATBFighter;
using EventBroker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerFighterStats : MonoBehaviour, IBattleMeterTick
    {
        public TextMeshProUGUI FighterName;
        public TextMeshProUGUI FighterHealth;
        public Slider BattleMeter;

        private FighterController _fighter;

        public void SetFighter(FighterController fighter)
        {
            _fighter = fighter;

            UpdateUiElements();
        }

        public void UpdateUiElements()
        {
            // TODO: Name, max health values aren't going to change a bunch...
            FighterName.text = _fighter.stats.fighterName;

            var currentHealth = ((int) _fighter.stats.currentHealth).ToString();
            var maxHealth = ((int) _fighter.stats.maxHealth).ToString();
            FighterHealth.text = currentHealth + " / " + maxHealth;

            BattleMeter.value = _fighter.stats.currentBattleMeterValue;
        }

        private void Start()
        {
            EventBroker.EventBroker.Instance.Subscribe(this);
        }

        public void NotifyBattleMeterTick(FighterController fighter)
        {
            if (fighter == _fighter) UpdateUiElements();
        }
    }
}
