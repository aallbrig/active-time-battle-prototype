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
        public FighterActionSequence actionSequence;
        public FighterActionAnimation egress;
    }
}