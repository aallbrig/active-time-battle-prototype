using System.Collections.Generic;
using Controllers;
using Utils;

namespace Managers
{
    public class FighterListsManager : Singleton<FighterListsManager>
    {
        public List<FighterController> playerFighters = new List<FighterController>();
        public List<FighterController> enemyFighters = new List<FighterController>();

        public void AddPlayerFighter(FighterController fighter)
        {
            if (!playerFighters.Contains(fighter)) playerFighters.Add(fighter);
        }

        public void RemovePlayerFighter(FighterController fighter)
        {
            if (playerFighters.Contains(fighter)) playerFighters.Remove(fighter);
        }

        public void ClearPlayerFighters() => ClearFighters(playerFighters);
        

        public void AddEnemyFighter(FighterController fighter)
        {
            if (!enemyFighters.Contains(fighter)) enemyFighters.Add(fighter);
        }

        public void RemoveEnemyFighter(FighterController fighter)
        {
            if (enemyFighters.Contains(fighter)) enemyFighters.Remove(fighter);
        }
        public void ClearEnemyFighters() => ClearFighters(enemyFighters);
        

        private void ClearFighters(List<FighterController> fighters)
        {
            fighters.ForEach(fighter => Destroy(fighter.gameObject));
            fighters.Clear();
        }
    }
}
