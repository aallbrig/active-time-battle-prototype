using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects.FighterActionAnimations
{
    [CreateAssetMenu(fileName = "Return To Origin", menuName = "active time battle/fighter action animations/ReturnToOrigin", order = 0)]
    public class ReturnToOrigin : FighterActionAnimation
    {
        public AnimatorTrigger runAnimation;
        public override IEnumerator Play(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            fighter.fighterAnimationController.UpdateAnimationTrigger(runAnimation.trigger);
            yield return fighter.agentController.SetDestination(fighter.startingPosition, 0);

            fighter.transform.rotation = fighter.startingRotation;
            fighter.fighterAnimationController.UpdateAnimationTrigger(fighter.startingTrigger);
        }
    }
}