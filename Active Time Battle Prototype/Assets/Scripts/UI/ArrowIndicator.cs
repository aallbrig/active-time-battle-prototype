using System.Collections.Generic;
using System.Linq;
using Controllers;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class ArrowIndicator : MonoBehaviour
    {
        public FighterListRuntimeSet playerFighters;
        public GameObject arrowIcon;
        private Transform _transform;
        private Transform _target;

        public void SetAboveTarget(FighterController fighter)
        {
            _target = fighter.transform;
            arrowIcon.SetActive(true);
        }

        public void SetAbovePlayerFighter(FighterController fighter)
        {
            if (playerFighters.fighters.Contains(fighter))
            {
                _target = fighter.transform;
                arrowIcon.SetActive(true);
            }
        }

        public void Hide(FighterController fighter, FighterAction action, List<FighterController> targets) => Hide();

        public void Hide() => arrowIcon.SetActive(false);

        private void Update()
        {
            // Targets mooooooove while you're selecting!
            if (!_target) return;

            _transform.position = _target.position;
        }

        private void Start()
        {
            _transform = transform;
        }
    }
}