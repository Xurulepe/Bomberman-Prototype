using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy.AI
{
    public class PatrolBehaviour : MonoBehaviour
    {
        [Header("Patrol Settings")]
        [SerializeField] private LayerMask wallsLayerMask;
        [SerializeField] private float wallCheckDistance = 0.5f;

        private Vector2 moveDirection = Vector2.right;
        private List<Vector2> directions = new List<Vector2>();

        public Vector2 MoveDirection => moveDirection;

        public event Action OnDirectionChanged;
        public event Action OnWallColision;

        private void OnEnable()
        {
            ChangeDirection();
        }

        private void Update()
        {
            CheckWalls();
        }

        public void SetPatrolSettings(float distance)
        {
            wallCheckDistance = distance;
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
