using System.Collections.Generic;
using Controllers;
using EventBroker.SubscriberInterfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerFighterStats : MonoBehaviour, IActivePlayerFighterSet, IPlayerTargetsSelected
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

        private void Start()
        {
            EventBroker.EventBroker.Instance.Subscribe((IActivePlayerFighterSet) this);
            EventBroker.EventBroker.Instance.Subscribe((IPlayerTargetsSelected) this);
        }

        private void OnDestroy()
        {
            EventBroker.EventBroker.Instance.Unsubscribe((IActivePlayerFighterSet) this);
            EventBroker.EventBroker.Instance.Unsubscribe((IPlayerTargetsSelected) this);
        }

        public void NotifyBattleMeterTick(FighterController fighter)
        {
            if (fighter == _fighter) UpdateUiElements();
        }

        public void NotifyActivePlayerFighterSet(FighterController fighter) => highlight.SetActive(fighter == _fighter);

        public void NotifyPlayerTargetsSelected(List<FighterController> targets) => highlight.SetActive(false);
    }
}
