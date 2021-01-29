using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.FighterInput
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(FighterInputStateController controller);
    }
}