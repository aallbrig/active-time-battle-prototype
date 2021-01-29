using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using Utils;

namespace Managers
{
    public class FighterListsManager : Singleton<FighterListsManager>
    {
        public FighterListRuntimeSet playerFighters;
        public FighterListRuntimeSet enemyFighters;

        public void AddPlayerFighter(FighterController fighter)
        {
            if (!playerFighters.fighters.Contains(fighter)) playerFighters.fighters.Add(fighter);
        }

        public void RemovePlayerFighter(FighterController fighter)
        {
            if (playerFighters.fighters.Contains(fighter)) playerFighters.fighters.Remove(fighter);
        }

        public void ClearPlayerFighters() => ClearFighters(playerFighters.fighters);
        

        public void AddEnemyFighter(FighterController fighter)
        {
            if (!enemyFighters.fighters.Contains(fighter)) enemyFighters.fighters.Add(fighter);
        }

        public void RemoveEnemyFighter(FighterController fighter)
        {
            if (enemyFighters.fighters.Contains(fighter)) enemyFighters.fighters.Remove(fighter);
        }
        public void ClearEnemyFighters() => ClearFighters(enemyFighters.fighters);
        

        private void ClearFighters(List<FighterController> fighters)
        {
            fighters.ForEach(fighter => Destroy(fighter.gameObject));
            fighters.Clear();
        }
    }
}
