using UnityEngine;

namespace Controllers
{
    public static class RTSToonAnimations
    {
        public static string Idle = "idle";
        public static string CombatIdle = "combat idle";
        public static string Walk = "walk";
        public static string Run = "run";
        public static string Charge = "charge";
        public static string Damage = "taking damage";
        public static string MeleeAttackA = "melee attack a";
        public static string MeleeAttackB = "melee attack b";
        public static string RangeAttack = "ranged attack";
        public static string DieA = "die a";
        public static string DieB = "die b";
    }

    public class RTSToonAnimationController : MonoBehaviour
    {
        public string startingTrigger = RTSToonAnimations.Idle;
        private Animator _animator;

        public string CurrentTrigger { get; private set; }

        public void Idling() => UpdateAnimationTrigger(RTSToonAnimations.Idle);
        public void IdlingInCombat() => UpdateAnimationTrigger(RTSToonAnimations.CombatIdle);
        public void Walking() => UpdateAnimationTrigger(RTSToonAnimations.Walk);
        public void Running() => UpdateAnimationTrigger(RTSToonAnimations.Run);
        public void Charging() => UpdateAnimationTrigger(RTSToonAnimations.Charge);
        public void TakingDamage() => UpdateAnimationTrigger(RTSToonAnimations.Damage);
        public void MeleeAttacking() =>
            UpdateAnimationTrigger(
                Random.Range(0f, 1f) > 0.5 ? RTSToonAnimations.MeleeAttackA : RTSToonAnimations.MeleeAttackB
            );
        public void AttackingAtRange() => UpdateAnimationTrigger(RTSToonAnimations.RangeAttack);
        public void Dying() => 
            UpdateAnimationTrigger(
                Random.Range(0f, 1f) > 0.5 ? RTSToonAnimations.DieA : RTSToonAnimations.DieB
            );

        public void UpdateAnimationTrigger(string triggerName)
        {
            // TODO: Why do I have to do this?
            if (_animator == null) _animator = GetComponent<Animator>();

            if (CurrentTrigger != null) _animator.ResetTrigger(triggerName);
            _animator.SetTrigger(triggerName);
            CurrentTrigger = triggerName;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (startingTrigger == RTSToonAnimations.CombatIdle) IdlingInCombat();
            else Idling();
        }
    }
}