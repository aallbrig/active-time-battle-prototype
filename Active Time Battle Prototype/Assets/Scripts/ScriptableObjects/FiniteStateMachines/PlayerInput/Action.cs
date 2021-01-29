using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(PlayerInputStateController controller);
    }
}