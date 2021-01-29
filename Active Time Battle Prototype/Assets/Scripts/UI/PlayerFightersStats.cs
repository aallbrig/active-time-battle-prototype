using Controllers;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class PlayerFightersStats : MonoBehaviour
    {
        public FighterListRuntimeSet playerFighters;
        public Transform playerStatsUiContainer;
        public PlayerFighterStats playerFighterStatsPrefab;

        public void Rerender(FighterController fighter)
        {
            Render();
        }

        private void OnEnable()
        {
            Render();
        }

        private void Render()
        {
            ClearFighterStatsContainer();

            playerFighters.fighters.ForEach(fighter =>
            {
                var fighterStats = Instantiate(playerFighterStatsPrefab, playerStatsUiContainer);
                fighterStats.SetFighter(fighter);
            });
        }

        private void ClearFighterStatsContainer()
        {
            foreach (Transform child in playerStatsUiContainer.transform)
                Destroy(child.gameObject);
        }
    }
}
