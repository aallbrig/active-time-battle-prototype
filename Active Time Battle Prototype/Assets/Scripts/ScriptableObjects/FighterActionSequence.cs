using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using ScriptableObjects.Data;
using ScriptableObjects.FighterActionAnimations;
using UnityEngine;

namespace ScriptableObjects
{

    [CreateAssetMenu(fileName = "new fighter action sequence", menuName = "active time battle/fighter action sequence", order = 0)]
    public class FighterActionSequence : FighterActionAnimation
    {
        [Tooltip("When should this happen?")] public ApplyActionEffectOptions whenAreEffectsApplied;

        [Header("(Optional) Animation options")]
        public List<AnimatorTrigger> actionAnimationTriggers;
        [Range(0f, 5f)]
        public float delayBetweenAnimationTriggers = 0.5f;
        public List<EffectFX> effects;

        public override IEnumerator Play(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            // Alias and lists
            var actionSeq = action.actionSequence;
            var beforeTriggerFX = actionSeq.effects.Where(effect => whenAreEffectsApplied == ApplyActionEffectOptions.BEFORE_TRIGGERS_PLAY).ToList();
            var forEachTrigger = actionSeq.effects.Where(effect => whenAreEffectsApplied == ApplyActionEffectOptions.FOR_EACH_TRIGGER).ToList();
            var afterTriggerFX = actionSeq.effects.Where(effect => whenAreEffectsApplied == ApplyActionEffectOptions.AFTER_TRIGGERS_PLAY).ToList();

            // BEFORE
            if (actionSeq.whenAreEffectsApplied == ApplyActionEffectOptions.BEFORE_TRIGGERS_PLAY)
            {
                yield return ApplyActionEffect(fighter, action, targets);
            }
            yield return InstantiateFX(fighter, action, targets, beforeTriggerFX);

            // FOR EACH
            for (var i = 0; i < action.actionSequence.actionAnimationTriggers.Count; i++)
            {
                if (actionSeq.whenAreEffectsApplied == ApplyActionEffectOptions.FOR_EACH_TRIGGER)
                {
                    yield return ApplyActionEffect(fighter, action, targets);
                }

                var trigger = actionSeq.actionAnimationTriggers[i];
                fighter.fighterAnimationController.UpdateAnimationTrigger(trigger.trigger);
                yield return InstantiateFX(fighter, action, targets, forEachTrigger);

                yield return new WaitForSeconds(delayBetweenAnimationTriggers);
            }

            // AFTER
            if (actionSeq.whenAreEffectsApplied == ApplyActionEffectOptions.AFTER_TRIGGERS_PLAY)
            {
                yield return ApplyActionEffect(fighter, action, targets);
            }
            yield return InstantiateFX(fighter, action, targets, afterTriggerFX);
        }

        private float CalculateEffect(float min, float max) => Random.Range(min, max);

        private IEnumerator ApplyActionEffect(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            var randomEffectVal = CalculateEffect(action.actionEffectMin, action.actionEffectMax);
    
            if (action.actionType == ActionType.Damaging)
            {
                targets.ForEach(target => target.TakeDamage(randomEffectVal));
            }
            else
            {
                targets.ForEach(target => target.Heal(randomEffectVal));
            }

            yield return null;
        }

        private IEnumerator InstantiateFX(FighterController fighter, FighterAction action, List<FighterController> targets, List<EffectFX> effects)
        {
            effects.ForEach(effect => {
                var prefab = effect.effectPrefab;

                if (prefab != null)
                {
                    var transformsLists = effect.effectTransforms;
                    transformsLists.ForEach(transforms =>
                    {
                        transforms.FindTransforms(fighter, action, targets).ForEach(transform =>
                        {
                            var instance = Instantiate(prefab, transform);
                            instance.transform.position += effect.effectVector3Offset.vector3;
                            fighter.StartCoroutine(DestroyInstanceAfter(instance, effect.effectPrefabLifetimeInSeconds));
                        });
                    });
                }
            });

            yield return null;
        }

        private IEnumerator DestroyInstanceAfter(GameObject effectInstance, float waitInSeconds)
        {
            yield return new WaitForSeconds(waitInSeconds);
            Destroy(effectInstance);
        }
    }
}