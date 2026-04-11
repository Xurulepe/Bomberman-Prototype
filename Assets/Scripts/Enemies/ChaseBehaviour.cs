using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy.AI
{
    public class ChaseBehaviour : MonoBehaviour
    {
        [Header("Chase Settings")]
        [SerializeField] private float chaseRange = 5f;

        private Transform target;
        private NavMeshAgent navMeshAgent;

        public event Action OnTargetLost;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }

        private void Update()
        {
            if (target == null)
                return;

            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget > chaseRange)
            {
                OnTargetLost?.Invoke();
            }
            else
            {
                navMeshAgent.SetDestination(target.position);
            }
        }

        public void SetChaseSettings(Transform target)
        {
            this.target = target;
        }

        private void OnEnable()
        {
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
        }

        private void OnDisable()
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
        }
    }
}
