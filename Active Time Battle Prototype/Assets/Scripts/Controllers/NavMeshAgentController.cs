using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public class NavMeshAgentController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private IEnumerator _agentIsAtDestinationCoroutine;
        private bool _needToDetect;

        public void SetDestination(Vector3 point, float stoppingDistance, Action onDestinationReachedCallback)
        {
            _needToDetect = true;
            _agent.stoppingDistance = stoppingDistance;
            _agent.destination = point;

            _agentIsAtDestinationCoroutine = AgentReachedDestination(_agent, onDestinationReachedCallback);

            StartCoroutine(_agentIsAtDestinationCoroutine);
        }

        private IEnumerator AgentReachedDestination(NavMeshAgent agent, Action callback)
        {
            while (_needToDetect)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance + 1)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            // Done
                            _needToDetect = false;
                        }
                    }
                }

                yield return new WaitForSeconds(0.25f);
            }

            callback?.Invoke();
        }


        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }
}