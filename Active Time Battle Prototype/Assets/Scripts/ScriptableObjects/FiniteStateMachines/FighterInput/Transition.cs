namespace ScriptableObjects.FiniteStateMachines.FighterInput
{
    [System.Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
    }
}