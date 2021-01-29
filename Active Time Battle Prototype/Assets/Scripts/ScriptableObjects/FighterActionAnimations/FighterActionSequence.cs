using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects.FighterActionAnimations
{
    [CreateAssetMenu(fileName = "Fighter Action Sequence", menuName = "active time battle/fighter action animations/FighterActionSequence", order = 0)]
    public class FighterActionSequence : FighterActionAnimation
    {
        public List<AnimatorTrigger> actionAnimationTriggers;
        public float waitInSeconds = 0.75f;
        public override IEnumerator Play(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            for (var i = 0; i < actionAnimationTriggers.Count; i++)
            {
                var trigger = actionAnimationTriggers[i];
                fighter.fighterAnimationController.UpdateAnimationTrigger(trigger.trigger);
                yield return new WaitForSeconds(waitInSeconds);
            }
        }
    }
}