using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Data;
using ScriptableObjects.Data;
using UnityEngine;

namespace ScriptableObjects.FighterActionAnimations
{
    [CreateAssetMenu(fileName = "Fighter Run Towards Targets", menuName = "active time battle/fighter action animations/FighterRunTowardsTargets", order = 0)]
    public class RunTowardsTargets : FighterActionAnimation
    {
        public override IEnumerator Play(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            var centerPoint = FighterController.FindCenterPoint(targets);

            // TODO How can I rewrite this to NOT use enum?
            // fighter.ingressAnimation?
            var playRunAnimation = new Action(() =>
            {
                if (action.actionType == ActionType.Healing) fighter.fighterAnimationController.Running();
                else fighter.fighterAnimationController.Charging();
            });
            // End TODO

            playRunAnimation();
            yield return fighter.agentController.SetDestination(centerPoint, action.ingressStoppingDistance);
        }
    }
}