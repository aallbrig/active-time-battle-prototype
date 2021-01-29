using Controllers.FiniteStateMachines;
using UnityEngine;

namespace ScriptableObjects.FiniteStateMachines.PlayerInput
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(PlayerInputStateController controller);
    }
}