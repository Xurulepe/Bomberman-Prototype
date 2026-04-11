using UnityEngine;

namespace Game.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Base Enemy Settings")]
        [SerializeField] protected float idleTime = 0.7f;
        [SerializeField] protected float idleInterval = 3f;
        [SerializeField] protected float moveSpeed = 2f;
        [SerializeField] protected LayerMask wallsLayerMask;
        [SerializeField] protected float wallCheckDistance = 0.5f;
        [SerializeField] protected EnemyState currentState;

        public EnemyState CurrentState => currentState;

        public void ChangeState(EnemyState newState)
        {
            Debug.Log($"Changing state from {currentState} to {newState}");
            currentState = newState;
        }
    }

    public enum EnemyState
    {
        Idle,
        Patroling,
        Chasing,
        Attacking
    }
}
