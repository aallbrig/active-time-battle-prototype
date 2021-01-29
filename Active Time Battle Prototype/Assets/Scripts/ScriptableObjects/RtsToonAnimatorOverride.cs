using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "animator override", menuName = "active time battle/animator override", order = 0)]
    public class RtsToonAnimatorOverride : ScriptableObject
    {
        public RuntimeAnimatorController animationController;
    }
}