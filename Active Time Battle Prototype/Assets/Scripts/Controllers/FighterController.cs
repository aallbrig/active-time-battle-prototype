using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Actions;
using Data.Fighters;
using UnityEngine;
using static Data.Actions.ActionType;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class FighterController : MonoBehaviour
    {
        public static event Action<FighterController, FighterAction, List<FighterController>> OnFighterAction;
        public static event Action<FighterController, float> OnFighterTakeDamage;
        public static event Action<FighterController, float> OnFighterHeal;
        public static event Action<FighterController> OnFighterDie;

        public FighterStats statsTemplate;
        public FighterStats stats;
        private Transform _transform;
        private FighterAnimationController _fighterAnimationController;
        private NavMeshAgentController _agentController;
        private IEnumerator _actionExecutionCoroutine;

        public List<FighterAction> GetActions() => stats.actions;

        public void ExecuteAction(FighterAction action, List<FighterController> targets, Action callback = null)
        {
            OnFighterAction?.Invoke(this, action, targets);
            _actionExecutionCoroutine = ExecuteActionCoroutine(action, targets, callback);
            StartCoroutine(_actionExecutionCoroutine);
        }

        private void TakeDamage(float damage)
        {
            OnFighterTakeDamage?.Invoke(this, damage);
            StartCoroutine(TakeDamageCoroutine(damage));
        }

        private void Heal(float heal)
        {
            OnFighterHeal?.Invoke(this, heal);
            stats.currentHealth = Mathf.Clamp(stats.currentHealth + heal, 0, stats.maxHealth);
        }

        public void RandomizeBattleMeter() => stats.currentBattleMeterValue = Random.Range(0f, 0.5f);
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
            // Wait for action animation to be complete
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
            stats.dead = true;
            OnFighterDie?.Invoke(this);
            _fighterAnimationController.Dying();
        }

        private IEnumerator TakeDamageCoroutine(float damage)
        {
            stats.currentHealth = Mathf.Clamp(stats.currentHealth - damage, 0, stats.maxHealth);
            var previousAnimation = _fighterAnimationController.CurrentTrigger;
            _fighterAnimationController.TakingDamage();
            yield return new WaitForSeconds(0.5f);

            if (stats.currentHealth == 0) Die();
            else _fighterAnimationController.UpdateAnimationTrigger(previousAnimation);
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
            RandomizeBattleMeter();
        }
    }
}