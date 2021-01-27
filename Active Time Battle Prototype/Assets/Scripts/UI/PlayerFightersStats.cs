using Controllers;
using Managers;
using UnityEngine;

namespace UI
{
    public class PlayerFightersStats : MonoBehaviour
    {
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

            FighterListsManager.Instance.playerFighters.ForEach(fighter =>
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
