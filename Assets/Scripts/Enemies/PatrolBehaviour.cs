using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy.AI
{
    public class PatrolBehaviour : MonoBehaviour
    {
        [Header("Patrol Settings")]
        [SerializeField] private LayerMask wallsLayerMask;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private LayerMask playerLayerMask;
        [SerializeField] private float playerDetectionDistance;  // move direction
        [SerializeField] private float minimumPlayerDetectionRange;  // around the enemy

        private bool hasChaseState;
        private Transform player;
        private float checkPlayerInterval;
        private float checkPlayerTimer;

        private Vector2 moveDirection = Vector2.right;
        private List<Vector2> directions = new List<Vector2>();

        public Vector2 MoveDirection => moveDirection;

        public event Action OnDirectionChanged;
        public event Action OnWallColision;
        public event Action OnPlayerDetected;

        private void OnEnable()
        {
            ChangeDirection();
        }

        private void Update()
        {
            CheckWalls();

            if (!hasChaseState)
            {
                return;
            }

            checkPlayerTimer += Time.deltaTime;

            if (checkPlayerTimer >= checkPlayerInterval)
            {
                CheckPlayer();
                checkPlayerTimer = 0f;
            }

            if (Vector2.Distance(transform.position, player.transform.position) <= minimumPlayerDetectionRange)
            {
                OnPlayerDetected?.Invoke();
            }
        }

        public void SetPatrolSettings(float distance)
        {
            wallCheckDistance = distance;
        }

        public void SetPlayerDetectionSettings(Transform player, float checkInterval, float detectionDistance, float minimumDetectionRange)
        {
            this.player = player;
            checkPlayerInterval = checkInterval;
            playerDetectionDistance = detectionDistance;
            minimumPlayerDetectionRange = minimumDetectionRange;
            hasChaseState = true;
        }

        private void CheckPlayer()
        {
            bool playerInSight = Physics2D.Raycast(transform.position, moveDirection, playerDetectionDistance, playerLayerMask);
            bool wallBlockingPlayer = Physics2D.Raycast(transform.position, moveDirection, playerDetectionDistance, wallsLayerMask);

            if (playerInSight && !wallBlockingPlayer)
            {
                OnPlayerDetected?.Invoke();
            }
        }

        private void CheckWalls()
        {
            if (Physics2D.Raycast(transform.position, moveDirection, wallCheckDistance, wallsLayerMask))
            {
                OnWallColision?.Invoke();

                ChangeDirection();
            }
        }

        public void ChangeDirection()
        {
            directions.Clear();

            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, wallCheckDistance, wallsLayerMask);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, wallCheckDistance, wallsLayerMask);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallsLayerMask);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallsLayerMask);

            if (hitUp.collider == null)
            {
                directions.Add(Vector2.up);
            }
            if (hitDown.collider == null)
            {
                directions.Add(Vector2.down);
            }
            if (hitLeft.collider == null)
            {
                directions.Add(Vector2.left);
            }
            if (hitRight.collider == null)
            {
                directions.Add(Vector2.right);
            }

            if (directions.Count > 0)
            {
                moveDirection = directions[UnityEngine.Random.Range(0, directions.Count)];
            }

            OnDirectionChanged?.Invoke();
        }
    }
}
