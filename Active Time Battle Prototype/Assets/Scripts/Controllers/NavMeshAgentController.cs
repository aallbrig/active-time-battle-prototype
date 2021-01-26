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

        public IEnumerator SetDestination(Vector3 point, float stoppingDistance, Action onDestinationReachedCallback = null)
        {
            _needToDetect = true;
            _agent.stoppingDistance = stoppingDistance;
            _agent.destination = point;

            _agentIsAtDestinationCoroutine = AgentReachedDestination(_agent, onDestinationReachedCallback);

            StartCoroutine(_agentIsAtDestinationCoroutine);

            return _agentIsAtDestinationCoroutine;
        }

        private IEnumerator AgentReachedDestination(NavMeshAgent agent, Action callback = null)
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


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }
}