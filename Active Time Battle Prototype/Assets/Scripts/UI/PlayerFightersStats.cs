using System.Collections.Generic;
using System.Linq;
using ATBFighter;
using UnityEngine;

namespace UI
{
    public class PlayerFightersStats : MonoBehaviour
    {
        public Transform playerStatsUiContainer;
        public PlayerFighterStats playerFighterStatsPrefab;
        private List<FighterController> _playerFighters = new List<FighterController>();
        private List<PlayerFighterStats> _playerFighterStatUiElements = new List<PlayerFighterStats>();

        public void SetPlayerFighters(List<FighterController> fighters)
        {
            Reset();
            _playerFighters = fighters;
            _playerFighterStatUiElements = _playerFighters.Select(fighter =>
            {
                var fighterStats = Instantiate(playerFighterStatsPrefab, playerStatsUiContainer);
                fighterStats.SetFighter(fighter);
                return fighterStats;
            }).ToList();
        }

        private void Reset()
        {
            foreach (Transform child in playerStatsUiContainer.transform)
                Destroy(child.gameObject);

            _playerFighters.Clear();
            _playerFighterStatUiElements.Clear();
        }
    }
}
