using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects.FighterActionAnimations
{
    public abstract class FighterActionAnimation : ScriptableObject
    {
        public abstract IEnumerator Play(FighterController fighter, FighterAction action, List<FighterController> targets);
    }
}