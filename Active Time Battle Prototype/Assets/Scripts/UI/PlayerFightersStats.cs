﻿using System.Collections.Generic;
using System.Linq;
using ATBFighter;
using UnityEngine;

namespace UI
{
    public class PlayerFightersStats : MonoBehaviour
    {
        public Transform playerStatsUiContainer;
        public PlayerFighterStats playerFighterStatsPrefab;
        private List<PlayerFighterStats> _playerFighterStatUiElements = new List<PlayerFighterStats>();

        public void SetPlayerFighters(List<FighterController> fighters)
        {
            ClearFighterStats();
            _playerFighterStatUiElements = fighters.Select(fighter =>
            {
                var fighterStats = Instantiate(playerFighterStatsPrefab, playerStatsUiContainer);
                fighterStats.SetFighter(fighter);
                return fighterStats;
            }).ToList();
        }

        private void ClearFighterStats()
        {
            foreach (Transform child in playerStatsUiContainer.transform)
                Destroy(child.gameObject);
        }
    }
}
