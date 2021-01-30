using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;

namespace ScriptableObjects.EffectTransforms
{
    [CreateAssetMenu(fileName = "Every target transform", menuName = "active time battle/fighter action effect transforms/EveryTargetTransform", order = 0)]
    public class EveryTargetTransform : ActionEffectTransforms
    {
        public override List<Transform> FindTransforms(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            return targets.Select(target => target.gameObject.transform).ToList();
        }
    }
}