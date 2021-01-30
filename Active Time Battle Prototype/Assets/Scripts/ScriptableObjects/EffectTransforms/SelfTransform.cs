using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects.EffectTransforms
{
    [CreateAssetMenu(fileName = "Self transform", menuName = "active time battle/fighter action effect transforms/SelfTransform", order = 0)]
    public class SelfTransform : ActionEffectTransforms
    {
        public override List<Transform> FindTransforms(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            return new List<Transform> { fighter.gameObject.transform };
        }
    }
}