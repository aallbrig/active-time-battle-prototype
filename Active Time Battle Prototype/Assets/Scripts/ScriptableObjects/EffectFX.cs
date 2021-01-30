using System.Collections.Generic;
using ScriptableObjects.Data;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new Effect FX", menuName = "active time battle/fighter action effect FX", order = 0)]
    public class EffectFX : ScriptableObject
    {
        [Header("FX options")]
        public GameObject effectPrefab;
        public List<ActionEffectTransforms> effectTransforms; // Use this for playing a particle FX (use polygon particle FX!)
        public Vector3_SO effectVector3Offset;
        [Range(0.1f, 8f)]
        public float effectPrefabLifetimeInSeconds;
        [Tooltip("When should this happen?")] public ApplyActionEffectOptions whenAreEffectsApplied;
    }
}