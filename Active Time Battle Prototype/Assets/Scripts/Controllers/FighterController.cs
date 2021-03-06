﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameEventSystem;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    [Serializable]
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
        public Vector3 startingPosition;
        public Quaternion startingRotation;
        public string startingTrigger;

        private Transform _transform;
        public FighterAnimationController fighterAnimationController;
        public NavMeshAgentController agentController;
        private IEnumerator _actionExecutionCoroutine;


        public static Vector3 FindCenterPoint(IReadOnlyCollection<FighterController> targets) =>
            targets.Select(target => target.transform.position)
                .Aggregate(new Vector3(), (acc, position) => acc + position)
                / targets.Count;

        public List<FighterAction> GetActions() => stats.actionSet.actions;

        public void ExecuteAction(FighterAction action, List<FighterController> targets)
        {
            _actionExecutionCoroutine = ExecuteActionCoroutine(action, targets);
            StartCoroutine(_actionExecutionCoroutine);
        }

        public void TakeDamage(float damage)
        {
            StartCoroutine(TakeDamageCoroutine(damage));
        }

        public void Heal(float heal)
        {
            if (stats.dead && heal > 0)
                fighterAnimationController.UpdateAnimationTrigger(startingTrigger);

            stats.currentHealth = Mathf.Clamp(stats.currentHealth + heal, 0, stats.maxHealth);
            stats.dead = stats.currentHealth == 0;

            if (fighterReceivesHeal != null) fighterReceivesHeal.Broadcast(this, heal);
        }

        public void RandomizeBattleMeter() => stats.currentBattleMeterValue = Random.Range(0f, 0.5f);
        public void ResetBattleMeter() => stats.currentBattleMeterValue = 0f;

        private IEnumerator ExecuteActionCoroutine(FighterAction action, List<FighterController> targets)
        {
            // INGRESS
            if (fighterActionStart != null) fighterActionStart.Broadcast(this, action, targets);
            yield return action.ingress.Play(this, action, targets);

            // ACT
            yield return action.actionSequence.Play(this, action, targets);
            if (fighterActionHandleEffects != null) fighterActionHandleEffects.Broadcast(this, action, targets);

            // EGRESS
            yield return action.egress.Play(this, action, targets);
            if (fighterActionComplete != null) fighterActionComplete.Broadcast(this, action, targets);

            // Action finished, reset battle meter
            ResetBattleMeter();
        }

        private void Die()
        {
            stats.dead = true;
            fighterAnimationController.Dying();

            if (fighterDie != null) fighterDie.Broadcast(this);
        }

        private IEnumerator TakeDamageCoroutine(float damage)
        {
            stats.currentHealth = Mathf.Clamp(stats.currentHealth - damage, 0, stats.maxHealth);
            var previousAnimation = fighterAnimationController.CurrentTrigger;
            fighterAnimationController.TakingDamage();
            if (fighterReceivesDamage != null) fighterReceivesDamage.Broadcast(this, damage);
            yield return new WaitForSeconds(0.5f);

            if (stats.currentHealth == 0) Die();
            else fighterAnimationController.UpdateAnimationTrigger(previousAnimation);
        }

        private void Start()
        {
            _transform = transform;
            startingPosition = _transform.position;
            startingRotation = _transform.rotation;
            fighterAnimationController = GetComponent<FighterAnimationController>();
            startingTrigger = fighterAnimationController.CurrentTrigger;
            agentController = GetComponent<NavMeshAgentController>();

            if (template != null)
            {
                stats = Instantiate(template);
            }
            RandomizeBattleMeter();
        }
    }
}