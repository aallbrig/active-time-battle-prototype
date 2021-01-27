using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Data;
using UnityEngine;

namespace UI
{
    public class PlayerFightersStats : MonoBehaviour
    {
        public Transform playerStatsUiContainer;
        public PlayerFighterStats playerFighterStatsPrefab;
        public FighterRuntimeSet playerFighters;
        private List<PlayerFighterStats> _playerFighterStatUiElements = new List<PlayerFighterStats>();

        private void OnEnable()
        {
            ClearFighterStats();
            _playerFighterStatUiElements = playerFighters.Select(fighter =>
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
