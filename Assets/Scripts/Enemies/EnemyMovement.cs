using UnityEngine;

namespace Game.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private Vector2 moveDirection;
        private float moveSpeed;

        private Rigidbody2D rb2D;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            rb2D.linearVelocity = moveDirection * moveSpeed;
        }

        public void SetMoveDirection(Vector2 direction)
        {
            moveDirection = direction.normalized;
        }

        public void SetMoveSettings(float speed)
        {
            moveSpeed = speed;
        }
    }
}
