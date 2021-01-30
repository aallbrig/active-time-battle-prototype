using System.Collections;
using System.Collections.Generic;
using Controllers;
using Data;
using ScriptableObjects.Data;
using ScriptableObjects.FighterActionAnimations;
using UnityEngine;

namespace ScriptableObjects
{
    // This doesn't seem like a good candidate for scriptable object because it is very closely related to the
    // action execution loop.
    public enum ApplyActionEffectOptions
    {
        BEFORE_TRIGGERS_PLAY,
        FOR_EACH_TRIGGER,
        AFTER_TRIGGERS_PLAY
    }

    [CreateAssetMenu(fileName = "new action effect", menuName = "active time battle/action effects/FighterActionEffects", order = 0)]
    public class FighterActionEffects : FighterActionAnimation
    {
        [Tooltip("When should this happen?")] public ApplyActionEffectOptions whenAreEffectsApplied;

        [Header("(Optional) Animation options")]
        public List<AnimatorTrigger> actionAnimationTriggers;
        [Range(0f, 5f)]
        public float delayBetweenAnimationTriggers = 0.5f;

        [Header("FX options")]
        public GameObject effectPrefab;
        public List<ActionEffectTransforms> effectTransforms; // Use this for playing a particle FX (use polygon particle FX!)
        public Vector3 effectVector3Offset = new Vector3(0, 0, 0);
        [Range(0.1f, 8f)]
        public float effectPrefabLifetimeInSeconds;

        public override IEnumerator Play(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            // Alias
            var actionSeq = action.actionSequence;

            // BEFORE
            if (actionSeq.whenAreEffectsApplied == ApplyActionEffectOptions.BEFORE_TRIGGERS_PLAY)
            {
                yield return InstantiateFX(fighter, action, targets);
                yield return ApplyActionEffect(fighter, action, targets);
            }

            // FOR EACH
            for (var i = 0; i < action.actionSequence.actionAnimationTriggers.Count; i++)
            {
                if (actionSeq.whenAreEffectsApplied == ApplyActionEffectOptions.FOR_EACH_TRIGGER)
                {
                    yield return InstantiateFX(fighter, action, targets);
                    yield return ApplyActionEffect(fighter, action, targets);
                }

                var trigger = actionSeq.actionAnimationTriggers[i];
                fighter.fighterAnimationController.UpdateAnimationTrigger(trigger.trigger);

                yield return new WaitForSeconds(delayBetweenAnimationTriggers);
            }

            // AFTER
            if (actionSeq.whenAreEffectsApplied == ApplyActionEffectOptions.AFTER_TRIGGERS_PLAY)
            {
                yield return InstantiateFX(fighter, action, targets);
                yield return ApplyActionEffect(fighter, action, targets);
            }
            
            yield return new WaitForSeconds(0.5f);
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

        private IEnumerator InstantiateFX(FighterController fighter, FighterAction action, List<FighterController> targets)
        {
            var effectsPrefab = action.actionSequence.effectPrefab;
            if (effectsPrefab == null) yield break;

            var transformsLists = action.actionSequence.effectTransforms;
            transformsLists.ForEach(transforms =>
            {
                transforms.FindTransforms(fighter, action, targets).ForEach(transform =>
                {
                    var instance = Instantiate(effectsPrefab, transform);
                    instance.transform.position += effectVector3Offset;
                    fighter.StartCoroutine(DestroyInstanceAfter(instance, action.actionSequence.effectPrefabLifetimeInSeconds));
                });
            });

            yield return null;
        }

        private IEnumerator DestroyInstanceAfter(GameObject effectInstance, float waitInSeconds)
        {
            yield return new WaitForSeconds(waitInSeconds);
            Destroy(effectInstance);
        }
    }


    [CreateAssetMenu(fileName = "new fighter action", menuName = "active time battle/fighter actions", order = 0)]
    public class FighterAction : ScriptableObject
    {
        public string actionName;
        public float actionEffectMin = 5;
        public float actionEffectMax = 12;
        public bool multiple = false;
        public bool canBeUsedOnDead = false;
        public ActionType actionType;
        public float ingressStoppingDistance = 2;

        public FighterActionAnimation ingress;
        public FighterActionEffects actionSequence;
        public FighterActionAnimation egress;
    }
}