using System.Collections.Generic;
using UnityEngine;

namespace ATBFighter
{
    [CreateAssetMenu(fileName = "new fighter", menuName = "fighter", order = 0)]
    public class ATBFighter_SO : ScriptableObject
    {
        public string fighterName;
        public float maxHealth;
        public float currentHealth;
        public float currentBattleMeterValue;
        public float secondsToMaxBattleMeterValue;
        // public GameObject model;
        // public Animator animator;
        public List<ATBFighterAction_SO> actions;
    }
}