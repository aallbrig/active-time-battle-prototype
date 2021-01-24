using UnityEngine;

namespace ATBFighter
{
    [CreateAssetMenu(fileName = "new fighter action", menuName = "fighter actions", order = 0)]
    public class ATBFighterAction_SO : ScriptableObject
    {
        public string actionName;
        public string animationTriggerName;
        public GameObject model;
        public float range = 1;
        public float actionEffect = 10;
        public ActionType actionType;
    }
}