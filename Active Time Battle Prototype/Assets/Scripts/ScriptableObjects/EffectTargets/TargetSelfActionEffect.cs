using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects.EffectTargets
{
    [CreateAssetMenu(fileName = "Self is target", menuName = "active time battle/fighter action effect targets/SelfIsTarget", order = 0)]
    public class SelfIsTarget : ActionEffectTargets
    {
       public override List<FighterController> FindTargets(FighterController fighter, FighterAction action, List<FighterController> targets)
       {
           return new List<FighterController> { fighter };
       }
    }
}