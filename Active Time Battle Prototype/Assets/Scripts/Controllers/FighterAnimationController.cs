using Data;
using UnityEngine;

namespace Controllers
{
    public class FighterAnimationController : MonoBehaviour
    {
        public RtsToonAnimationManifest animationManifest;
        private Animator _animator;

        public string CurrentTrigger { get; private set; }

        public void Idling() => UpdateAnimationTrigger(animationManifest.idle);
        public void IdlingInCombat() => UpdateAnimationTrigger(animationManifest.combatIdle);
        public void Walking() => UpdateAnimationTrigger(animationManifest.walk);
        public void Running() => UpdateAnimationTrigger(animationManifest.run);
        public void Charging() => UpdateAnimationTrigger(animationManifest.charge);
        public void TakingDamage() => UpdateAnimationTrigger(animationManifest.takeDamage);
        public void Slash() =>
            UpdateAnimationTrigger(
                Random.Range(0f, 1f) > 0.5 ? animationManifest.slashAttackA : animationManifest.slashAttackB
            );
        public void Stab() => UpdateAnimationTrigger(animationManifest.stabAttack);
        public void RangedAttack() => UpdateAnimationTrigger(animationManifest.rangedAttack);
        public void Dying() => 
            UpdateAnimationTrigger(Random.Range(0f, 1f) > 0.5 ? animationManifest.deathA : animationManifest.deathB);

        public void UpdateAnimationTrigger(string triggerName)
        {
            if (CurrentTrigger != null) _animator.ResetTrigger(triggerName);
            _animator.SetTrigger(triggerName);
            CurrentTrigger = triggerName;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = animationManifest.animationController;
            Idling();
        }
    }
}