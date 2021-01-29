using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(FighterInputStateController controller);
    }
}