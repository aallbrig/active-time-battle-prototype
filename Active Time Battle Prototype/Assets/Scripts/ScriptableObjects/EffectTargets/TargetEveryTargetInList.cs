using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects.EffectTargets
{
    [CreateAssetMenu(fileName = "Every target in target list", menuName = "active time battle/fighter action effect targets/EveryTargetInTargetList", order = 0)]
    public class EveryTargetInTargetList : ActionEffectTargets
    {
        public override List<FighterController> FindTargets(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            return targets;
        }
    }
}