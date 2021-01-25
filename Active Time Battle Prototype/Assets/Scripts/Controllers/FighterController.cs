using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;
using static ATBFighter.ActionType;

namespace ATBFighter
{
    public class FighterController : MonoBehaviour
    {
        public ATBFighter_SO statsTemplate;
        public ATBFighter_SO stats;
        private RTSToonAnimationController _rtsAnimator;
        private NavMeshAgentController _agentController;

        public List<ATBFighterAction_SO> GetActions() => stats.actions;

        public void ExecuteAction(ATBFighterAction_SO action, List<FighterController> targets)
        {
            var originPosition = transform.position;
            var originRotation = transform.rotation;
            var originTrigger = _rtsAnimator.CurrentTrigger;

            var centerPoint = FindCenterPoint(targets);

            var returnToOrigin = new Action(() =>
            {
                _rtsAnimator.Running();
                _agentController.SetDestination(originPosition, 0, () =>
                {
                    transform.rotation = originRotation;
                    _rtsAnimator.UpdateAnimationTrigger(originTrigger);
                });
            });
            

            if (action.actionType == Healing)
            {
                _rtsAnimator.Running();
                _agentController.SetDestination(
                    centerPoint,
                    action.range,
                    () =>
                    {
                        _rtsAnimator.UpdateAnimationTrigger(action.animationTriggerName);
                        targets.ForEach(target => target.Heal(action.actionEffect));
                        returnToOrigin();
                    });
            }
            else
            {
                _rtsAnimator.Charging();
                _agentController.SetDestination(
                    centerPoint,
                    action.range,
                    () =>
                    {
                        _rtsAnimator.UpdateAnimationTrigger(action.animationTriggerName);
                        targets.ForEach(target => target.TakeDamage(action.actionEffect));
                        returnToOrigin();
                    });
            }
        }

        public void TakeDamage(float damage)
        {
            stats.currentHealth = Mathf.Clamp(stats.currentHealth - damage, 0, stats.maxHealth);
            _rtsAnimator.TakingDamage();

            if (stats.currentHealth == 0) Die();
        }

        public void Heal(float heal)
        {
            stats.currentHealth = Mathf.Clamp(stats.currentHealth + heal, 0, stats.maxHealth);
        }

        public void ResetBattleMeter()
        {
            stats.currentBattleMeterValue = 0f;
        }

        private static Vector3 FindCenterPoint(IReadOnlyCollection<FighterController> targets) =>
            targets.Select(target => target.transform.position)
                .Aggregate(new Vector3(), (acc, position) => acc + position)
                / targets.Count;

        private void Die()
        {
            Debug.Log("Blarg I " + stats.fighterName + " am dead X_X ");
        }

        private void Start()
        {
            _rtsAnimator = GetComponent<RTSToonAnimationController>();
            _agentController = GetComponent<NavMeshAgentController>();

            if (statsTemplate != null)
            {
                stats = Instantiate(statsTemplate);
            }
        }
    }
}