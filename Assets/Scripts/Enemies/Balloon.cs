using Game.Enemy.AI;
using UnityEngine;

namespace Game.Enemy
{
    public class Balloon : Enemy
    {
        [Header("Balloon Settings")]
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private IdleBehaviour idleBehaviour;
        [SerializeField] private PatrolBehaviour patrolBehaviour;
        [SerializeField] private ChaseBehaviour chaseBehaviour;

        [Header("Player Detection Settings")]
        [SerializeField] private Transform playerTarget;
        [SerializeField] private float playerDetectionDistance;  // move direction
        [SerializeField] private float minimumPlayerDetectionRange;  // around the enemy
        [SerializeField] private float checkPlayerInterval;

        private void Awake()
        {
            if (playerTarget == null)
            {
                playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            }

            enemyMovement.SetMoveSettings(moveSpeed);
            idleBehaviour.SetIdleSettings(idleTime, idleInterval);
            patrolBehaviour.SetPatrolSettings(wallCheckDistance);
            patrolBehaviour.SetPlayerDetectionSettings(playerTarget, checkPlayerInterval, playerDetectionDistance, minimumPlayerDetectionRange);
            chaseBehaviour.SetChaseSettings(playerTarget);
        }

        private void Start()
        {
            patrolBehaviour.OnDirectionChanged += HandleDirectionChanged;
        }

        private void Update()
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    enemyMovement.enabled = true;
                    enemyMovement.SetMoveDirection(Vector2.zero);

                    break;

                case EnemyState.Patroling:
                    enemyMovement.enabled = true;

                    break;

                case EnemyState.Chasing:
                    enemyMovement.enabled = false;

                    break;
            }
        }

        private void HandleDirectionChanged()
        {
            enemyMovement.SetMoveDirection(patrolBehaviour.MoveDirection);
        }
    }
}

