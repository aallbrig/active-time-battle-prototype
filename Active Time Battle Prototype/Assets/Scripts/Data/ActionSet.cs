using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "new fighter action set", menuName = "active time battle/fighter action set", order = 0)]
    public class ActionSet : ScriptableObject
    {
        public List<FighterAction> actions;
    }
}