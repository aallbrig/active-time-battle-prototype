namespace ScriptableObjects.FiniteStateMachines.PlayerInput
{
    [System.Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
    }
}