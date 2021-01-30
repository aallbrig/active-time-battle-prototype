using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;

namespace ScriptableObjects.FighterActionAnimations
{
    [CreateAssetMenu(fileName = "Fighter Action Sequence", menuName = "active time battle/fighter action animations/FighterActionSequence", order = 0)]
    public class FighterActionSequence : FighterActionAnimation
    {
        // When does effect happen?
        // Are there any FX to instantiate?
        // What animation triggers need to play?
        public List<FighterActionEffects> actionEffectOptions = new List<FighterActionEffects>();

        public override IEnumerator Play(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            for (int i = 0; i < actionEffectOptions.Count; i++)
            {
                var options = actionEffectOptions[i];
                yield return HandleActionEffectOption(fighter, action, targets, options);
            }
        }

        private IEnumerator HandleActionEffectOption(
            FighterController fighter, FighterAction action, List<FighterController> targets, FighterActionEffects options
        )
        {
            // BEFORE
            // if (options.whenEffectIsExecuted == ApplyActionEffectOptions.BEFORE_TRIGGERS_PLAY)
            {
                
                // if (options.damaging) targets.ForEach();
            }

            for (int i = 0; i < options.actionAnimationTriggers.Count; i++)
            {
                var trigger = options.actionAnimationTriggers[i];
                fighter.fighterAnimationController.UpdateAnimationTrigger(trigger.trigger);
                yield return new WaitForSeconds(trigger.triggerDuration);
            }
            
            // AFTER
        }
    }
}