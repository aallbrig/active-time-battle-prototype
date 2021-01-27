using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data
{
    [CreateAssetMenu(fileName = "fighter runtime set", menuName = "active time battle/fighter set", order = 0)]
    public class FighterRuntimeSet : ScriptableObject
    {
        public List<FighterController> fighters = new List<FighterController>();

        public void Add(FighterController item)
        {
            if (!fighters.Contains(item)) fighters.Add(item);
        }

        public void Remove(FighterController item)
        {
            if (fighters.Contains(item)) fighters.Remove(item);
        }

        public void Clear() => fighters.Clear();
        public IEnumerable<FighterController> Where(Func<FighterController, bool> predicate) => fighters.Where(predicate);

        public void ForEach(Action<FighterController> forEachAction) => fighters.ForEach(forEachAction);
        public bool Contains(FighterController fighter) => fighters.Contains(fighter);

        public FighterController RandomAliveFighter() {
            var validFighters = fighters.Where(fighter => !fighter.stats.dead).ToList();
            return validFighters[Random.Range(0, validFighters.Count)];
        }

        public IEnumerable<FighterStats> Select<FighterStats>(Func<FighterController, FighterStats> func) => fighters.Select(func);

        public int Count => fighters.Count;
        public float Aggregate (float seed, Func<float, FighterController, float> reducer) => fighters.Aggregate(seed, reducer);

        public List<FighterController> ToList() => fighters;

        private void Awake() => fighters.Clear();
        private void OnDisable() => fighters.Clear();

        private void OnDestroy() => fighters.Clear();
    }
}
