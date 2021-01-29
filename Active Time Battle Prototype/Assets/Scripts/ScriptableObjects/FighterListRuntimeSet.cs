using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new fighter list", menuName = "active time battle/fighter list", order = 0)]
    public class FighterListRuntimeSet : ScriptableObject
    {
        public List<FighterController> fighters = new List<FighterController>();

        private void OnEnable() => fighters.Clear();
        private void OnDisable() => fighters.Clear();
        private void OnDestroy() => fighters.Clear();
    }
}