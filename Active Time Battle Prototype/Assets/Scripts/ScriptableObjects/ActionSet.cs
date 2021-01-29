using System.Collections.Generic;
using Data;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new fighter action set", menuName = "active time battle/fighter action set", order = 0)]
    public class ActionSet : ScriptableObject
    {
        public List<FighterAction> actions;
    }
}