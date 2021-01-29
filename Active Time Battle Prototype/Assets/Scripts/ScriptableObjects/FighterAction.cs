﻿using Data;
using ScriptableObjects.FighterActionAnimations;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new fighter action", menuName = "active time battle/fighter actions", order = 0)]
    public class FighterAction : ScriptableObject
    {
        public string actionName;
        public float range = 2;
        public float actionEffectMin = 5;
        public float actionEffectMax = 12;
        public bool multiple = false;
        public bool canBeUsedOnDead = false;
        public ActionType actionType;
        public FighterActionAnimation ingress;
        public FighterActionAnimation act;
        public FighterActionAnimation egress;
    }
}