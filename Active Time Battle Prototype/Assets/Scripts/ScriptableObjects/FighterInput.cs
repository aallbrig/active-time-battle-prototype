using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "active time battle/fighter input")]
    public class FighterInput : ScriptableObject
    {
        public FighterController fighter;
        public FighterAction action;
        public List<FighterController> targets = new List<FighterController>();

        public bool IsReset()
        {
            return fighter == null && action == null && targets.Count == 0;
        }

        public void ResetInput()
        {
            fighter = null;
            action = null;
            targets = new List<FighterController>();
        }

        private void OnEnable() => ResetInput();
        private void OnDisable() => ResetInput();
    }
}