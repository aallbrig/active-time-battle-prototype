using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "animation manifest", menuName = "active time battle/animation manifest", order = 0)]
    public class RtsToonAnimationManifest : ScriptableObject
    {
        // Trigger names for each animation
        // (used in Fighter Animation Controller)
        public string idle = "Idle";
        public string combatIdle = "Combat Idle";
        public string walk = "Walk";
        public string combatWalk = "Combat Walk";
        public string run = "Run";
        public string charge = "Charge";  // aka combatRun
        public string rangedAttack = "Ranged Attack";
        public string slashAttackA = "Slash Attack A";
        public string slashAttackB = "Slash Attack B";
        public string stabAttack = "Stab Attack";  // aka spear thrust but can be used with swords
        public string castTarget = "Cast Target";  // cast directionally
        public string castMultiple = "Cast Multiple";  // raise hands
        public string castChannel = "Cast Channel";  // WOLOO
        public string takeDamage = "Take Damage";
        public string deathA = "Death A";
        public string deathB = "Death B";
    }
}