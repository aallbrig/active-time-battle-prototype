using System.Collections.Generic;
using Controllers;
using EventBroker.SubscriberInterfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerFighterStats : MonoBehaviour, IBattleMeterTick, IActivePlayerFighterSet, IPlayerTargetsSelected
    {
        public TextMeshProUGUI FighterName;
        public TextMeshProUGUI FighterHealth;
        public Slider BattleMeter;
        public GameObject Highlight;

        private FighterController _fighter;

        public void SetFighter(FighterController fighter)
        {
            _fighter = fighter;

            UpdateUiElements();
        }

        private void UpdateUiElements()
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
            EventBroker.EventBroker.Instance.Subscribe((IBattleMeterTick) this);
            EventBroker.EventBroker.Instance.Subscribe((IActivePlayerFighterSet) this);
            EventBroker.EventBroker.Instance.Subscribe((IPlayerTargetsSelected) this);
        }

        private void OnDestroy()
        {
            EventBroker.EventBroker.Instance.Unsubscribe((IBattleMeterTick) this);
            EventBroker.EventBroker.Instance.Unsubscribe((IActivePlayerFighterSet) this);
            EventBroker.EventBroker.Instance.Unsubscribe((IPlayerTargetsSelected) this);
        }

        public void NotifyBattleMeterTick(FighterController fighter)
        {
            UpdateUiElements();
        }

        public void NotifyActivePlayerFighterSet(FighterController fighter) => Highlight.SetActive(fighter == _fighter);

        public void NotifyPlayerTargetsSelected(List<FighterController> targets) => Highlight.SetActive(false);
    }
}
