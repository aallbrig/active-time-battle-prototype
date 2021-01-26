using System;
using System.Collections.Generic;
using System.Linq;
using Data.Actions;
using Data.Fighters;
using UnityEngine;
using static Data.Actions.ActionType;

namespace Controllers
{
    public class FighterController : MonoBehaviour
    {
        public FighterStats statsTemplate;
        public FighterStats stats;
        private FighterAnimationController _fighterAnimationController;
        private NavMeshAgentController _agentController;

        public List<FighterAction> GetActions() => stats.actions;

        public void ExecuteAction(FighterAction action, List<FighterController> targets, Action callback = null)
        {
            var originPosition = transform.position;
            var originRotation = transform.rotation;
            var originTrigger = _fighterAnimationController.CurrentTrigger;

            var centerPoint = FindCenterPoint(targets);

            var returnToOrigin = new Action(() =>
            {
                _fighterAnimationController.Running();
                _agentController.SetDestination(originPosition, 0, () =>
                {
                    transform.rotation = originRotation;
                    _fighterAnimationController.UpdateAnimationTrigger(originTrigger);
                    callback?.Invoke();
                });
            });
            

            if (action.actionType == Healing)
            {
                _fighterAnimationController.Running();
                _agentController.SetDestination(
                    centerPoint,
                    action.range,
                    () =>
                    {
                        _fighterAnimationController.UpdateAnimationTrigger(action.animationTriggerName);
                        targets.ForEach(target => target.Heal(action.actionEffect));
                        returnToOrigin();
                    });
            }
            else
            {
                _fighterAnimationController.Charging();
                _agentController.SetDestination(
                    centerPoint,
                    action.range,
                    () =>
                    {
                        _fighterAnimationController.Slash();
                        // _fighterAnimationController.UpdateAnimationTrigger(action.animationTriggerName);
                        targets.ForEach(target => target.TakeDamage(action.actionEffect));
                        returnToOrigin();
                    });
            }
        }

        public void TakeDamage(float damage)
        {
            stats.currentHealth = Mathf.Clamp(stats.currentHealth - damage, 0, stats.maxHealth);
            _fighterAnimationController.TakingDamage();

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
            _fighterAnimationController = GetComponent<FighterAnimationController>();
            _agentController = GetComponent<NavMeshAgentController>();

            if (statsTemplate != null)
            {
                stats = Instantiate(statsTemplate);
            }
        }
    }
}