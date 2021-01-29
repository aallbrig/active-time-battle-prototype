using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using GameEventSystem;
using ScriptableObjects;
using UnityEngine;
using static Data.ActionType;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class FighterController : MonoBehaviour
    {
        public FighterActionExecuteGameEvent fighterActionStart;
        public FighterActionExecuteGameEvent fighterActionHandleEffects;
        public FighterActionExecuteGameEvent fighterActionComplete;
        public FighterActionEffectGameEvent fighterReceivesDamage;
        public FighterActionEffectGameEvent fighterReceivesHeal;

        public FighterGameEvent fighterDie;

        public Fighter template;
        public Fighter stats;
        private Transform _transform;
        private FighterAnimationController _fighterAnimationController;
        private NavMeshAgentController _agentController;
        private IEnumerator _actionExecutionCoroutine;

        public List<FighterAction> GetActions() => stats.actionSet.actions;

        public void ExecuteAction(FighterAction action, List<FighterController> targets)
        {
            _actionExecutionCoroutine = ExecuteActionCoroutine(action, targets);
            StartCoroutine(_actionExecutionCoroutine);
        }

        private void TakeDamage(float damage)
        {
            StartCoroutine(TakeDamageCoroutine(damage));
        }

        private void Heal(float heal)
        {
            stats.currentHealth = Mathf.Clamp(stats.currentHealth + heal, 0, stats.maxHealth);
            if (fighterReceivesHeal != null) fighterReceivesHeal.Broadcast(this, heal);
        }

        public void RandomizeBattleMeter() => stats.currentBattleMeterValue = Random.Range(0f, 0.5f);
        public void ResetBattleMeter() => stats.currentBattleMeterValue = 0f;

        private IEnumerator ExecuteActionCoroutine(FighterAction action, List<FighterController> targets)
        {
            if (fighterActionStart != null) fighterActionStart.Broadcast(this, action, targets);
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
            switch (action.actionAnimation)
            {
                case ActionAnimation.SlashAttack:
                    _fighterAnimationController.Slashing();
                    break;
                case ActionAnimation.StabAttack:
                    _fighterAnimationController.Stabbing();
                    break;
                case ActionAnimation.RangedAttack:
                    _fighterAnimationController.AttackingAtRange();
                    break;
                case ActionAnimation.TargetCast:
                    _fighterAnimationController.CastingAtTarget();
                    break;
                case ActionAnimation.MultipleCast:
                    _fighterAnimationController.CastingAtMultiple();
                    break;
                case ActionAnimation.ChannelCast:
                    _fighterAnimationController.ChannelCasting();
                    break;
            }

            // Handle action effects

            var actionEffect = Random.Range(action.actionEffectMin, action.actionEffectMax);
            if (action.actionType == Healing) targets.ForEach(target => target.Heal(actionEffect));
            else targets.ForEach(target => target.TakeDamage(actionEffect));

            if (fighterActionHandleEffects != null) fighterActionHandleEffects.Broadcast(this, action, targets);
            // Wait for action animation to be complete
            yield return new WaitForSeconds(0.5f);

            playRunAnimation();
            yield return _agentController.SetDestination(originPosition, 0);

            transform.rotation = originRotation;
            _fighterAnimationController.UpdateAnimationTrigger(originTrigger);

            if (fighterActionComplete != null) fighterActionComplete.Broadcast(this, action, targets);
            ResetBattleMeter();
        }

        private static Vector3 FindCenterPoint(IReadOnlyCollection<FighterController> targets) =>
            targets.Select(target => target.transform.position)
                .Aggregate(new Vector3(), (acc, position) => acc + position)
                / targets.Count;

        private void Die()
        {
            stats.dead = true;
            _fighterAnimationController.Dying();

            if (fighterDie != null) fighterDie.Broadcast(this);
        }

        private IEnumerator TakeDamageCoroutine(float damage)
        {
            stats.currentHealth = Mathf.Clamp(stats.currentHealth - damage, 0, stats.maxHealth);
            var previousAnimation = _fighterAnimationController.CurrentTrigger;
            _fighterAnimationController.TakingDamage();
            if (fighterReceivesDamage != null) fighterReceivesDamage.Broadcast(this, damage);
            yield return new WaitForSeconds(0.5f);

            if (stats.currentHealth == 0) Die();
            else _fighterAnimationController.UpdateAnimationTrigger(previousAnimation);
        }

        private void Start()
        {
            _transform = transform;
            _fighterAnimationController = GetComponent<FighterAnimationController>();
            _agentController = GetComponent<NavMeshAgentController>();

            if (template != null)
            {
                stats = Instantiate(template);
            }
            RandomizeBattleMeter();
        }
    }
}