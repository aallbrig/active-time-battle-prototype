using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class FighterTester : MonoBehaviour
    {
        public FighterController fighter;
        public List<FighterController> targets;

        private Vector3 FindCenterPoint(List<FighterController> targets)
        {
            return targets.Select(target => ((Component) target).transform.position)
                .Aggregate(new Vector3(), (acc, transform) => acc += transform)
                / targets.Count;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
            {
                var actions = fighter.GetActions();
                var randomAction = actions[Random.Range(0, actions.Count)];
                fighter.ExecuteAction(randomAction, targets);
            }
        }

        private void OnDrawGizmos()
        {
            if (targets == null || (targets.Count > 0 && targets[0] == null)) return;
            var centerPoint = FindCenterPoint(targets);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(centerPoint, 0.5f);
        }
    }
}