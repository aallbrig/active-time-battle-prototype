using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class ActionEffectTransforms : ScriptableObject
    {
        public abstract List<Transform> FindTransforms(FighterController fighter, FighterAction action, List<FighterController> targets);
    }
    
}