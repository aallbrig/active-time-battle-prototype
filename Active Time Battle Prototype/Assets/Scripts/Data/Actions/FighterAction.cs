using UnityEngine;

namespace Data.Actions
{
    [CreateAssetMenu(fileName = "new fighter action", menuName = "active time battle/fighter actions", order = 0)]
    public class FighterAction : ScriptableObject
    {
        public string actionName;
        public float range = 1;
        public float actionEffect = 10;
        public ActionType actionType;
    }
}