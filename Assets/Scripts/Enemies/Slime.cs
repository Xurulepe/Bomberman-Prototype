using UnityEngine;
using Game.Enemy.AI;

namespace Game.Enemy
{
    public class Slime : Enemy
    {
        [Header("Slime Settings")]
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private IdleBehaviour idleBehaviour;
        [SerializeField] private PatrolBehaviour patrolBehaviour;

        private void Awake()
        {
            enemyMovement.SetMoveSettings(moveSpeed);
            idleBehaviour.SetIdleSettings(idleTime, idleInterval);
            patrolBehaviour.SetPatrolSettings(wallCheckDistance);
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
                    enemyMovement.SetMoveDirection(Vector2.zero);

                    break;

                case EnemyState.Patroling:


                    break;
            }
        }

        private void HandleDirectionChanged()
        {
            enemyMovement.SetMoveDirection(patrolBehaviour.MoveDirection);
        }
    }
}
