using System.Collections.Generic;
using System.Linq;
using ATBFighter;
using UI;
using UnityEngine;

namespace Controllers
{
    public class FighterTester : MonoBehaviour
    {
        public FighterController fighter;
        public List<FighterController> targets;
        public PlayerActions playerActions;
        public PlayerTargets playerTargets;
        public PlayerFightersStats playerFightersStats;

        private Vector3 FindCenterPoint(List<FighterController> targets)
        {
            return targets.Select(target => target.transform.position)
                .Aggregate(new Vector3(), (acc, transform) => acc += transform)
                / targets.Count;
        }

        private void OnEnable()
        {
            var actions = fighter.GetActions();
            playerActions.SetActions(actions);
            playerTargets.SetTargets(targets);
            playerFightersStats.SetPlayerFighters(new List<FighterController> { fighter });
        }

        private void OnDrawGizmos()
        {
            var centerPoint = FindCenterPoint(targets);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(centerPoint, 0.5f);
        }
    }
}