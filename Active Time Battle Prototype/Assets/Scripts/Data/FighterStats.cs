using System.Collections.Generic;
using Data.Actions;
using UnityEngine;

namespace Data.Fighters
{
    [CreateAssetMenu(fileName = "new fighter", menuName = "active time battle/fighter", order = 0)]
    public class FighterStats : ScriptableObject
    {
        public string fighterName;
        public float maxHealth;
        public float currentHealth;
        public bool dead;
        public float currentBattleMeterValue;
        public float secondsToMaxBattleMeterValue;
        // public GameObject model;
        // public Animator animator;
        public List<FighterAction> actions;
    }
}