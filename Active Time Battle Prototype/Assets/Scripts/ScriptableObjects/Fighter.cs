using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new fighter", menuName = "active time battle/fighter", order = 0)]
    public class Fighter : ScriptableObject
    {
        public string fighterName;
        public float maxHealth;
        public float currentHealth;
        public bool dead;
        public float currentBattleMeterValue;
        public float secondsToMaxBattleMeterValue;
        public ActionSet actionSet;
    }
}