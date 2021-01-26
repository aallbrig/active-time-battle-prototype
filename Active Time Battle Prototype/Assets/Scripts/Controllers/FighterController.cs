using System;
using System.Collections;
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
        private Transform _transform;
        private FighterAnimationController _fighterAnimationController;
        private NavMeshAgentController _agentController;
        private IEnumerator _actionExecutionCoroutine;

        public List<FighterAction> GetActions() => stats.actions;

        public void ExecuteAction(FighterAction action, List<FighterController> targets, Action callback = null)
        {
            _actionExecutionCoroutine = ExecuteActionCoroutine(action, targets, callback);
            StartCoroutine(_actionExecutionCoroutine);
        }

        public void TakeDamage(float damage)
        {
            stats.currentHealth = Mathf.Clamp(stats.currentHealth - damage, 0, stats.maxHealth);
            _fighterAnimationController.TakingDamage();

            if (stats.currentHealth == 0) Die();
        }

        public void Heal(float heal) => stats.currentHealth = Mathf.Clamp(stats.currentHealth + heal, 0, stats.maxHealth);

        public void ResetBattleMeter() => stats.currentBattleMeterValue = 0f;

        private IEnumerator ExecuteActionCoroutine(FighterAction action, List<FighterController> targets, Action callback)
        {
            var originPosition = _transform.position;
            var originRotation = _transform.rotation;
            var originTrigger = _fighterAnimationController.CurrentTrigger;
            var centerPoint = FindCenterPoint(targets);

            var playRunAnimation = new Action(() =>
            {
                if (action.actionType == Healing) _fighterAnimationController.Running();
                else _fighterAnimationController.Charging();
            });

            playRunAnimation();
            yield return _agentController.SetDestination(centerPoint, action.range);

            // Play action animation
            if (action.actionType == Healing) _fighterAnimationController.Slashing();
            else _fighterAnimationController.CastingAtMultiple();

            // Handle action effects
            if (action.actionType == Healing) targets.ForEach(target => target.Heal(action.actionEffect));
            else targets.ForEach(target => target.TakeDamage(action.actionEffect));
            // Wait for action animation to be complete
            // TODO:
            yield return new WaitForSeconds(0.5f);

            playRunAnimation();
            yield return _agentController.SetDestination(originPosition, 0);

            transform.rotation = originRotation;
            _fighterAnimationController.UpdateAnimationTrigger(originTrigger);
            callback?.Invoke();
        }

        private static Vector3 FindCenterPoint(IReadOnlyCollection<FighterController> targets) =>
            targets.Select(target => target.transform.position)
                .Aggregate(new Vector3(), (acc, position) => acc + position)
                / targets.Count;

        private void Die()
        {
            // TODO: Broadcast death event
            Debug.Log("Blarg I " + stats.fighterName + " am dead X_X ");
            _fighterAnimationController.Dying();
        }

        private void Start()
        {
            _transform = transform;
            _fighterAnimationController = GetComponent<FighterAnimationController>();
            _agentController = GetComponent<NavMeshAgentController>();

            if (statsTemplate != null)
            {
                stats = Instantiate(statsTemplate);
            }
        }
    }
}