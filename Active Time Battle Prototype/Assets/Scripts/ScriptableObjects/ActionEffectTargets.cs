using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class ActionEffectTargets : ScriptableObject
    {
        public abstract List<FighterController> FindTargets(FighterController fighter, FighterAction action, List<FighterController> targets);
    }
}