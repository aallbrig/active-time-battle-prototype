using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new animation trigger", menuName = "active time battle/animator trigger", order = 0)]
    public class AnimatorTrigger : ScriptableObject
    {
        public string trigger;
    }
}