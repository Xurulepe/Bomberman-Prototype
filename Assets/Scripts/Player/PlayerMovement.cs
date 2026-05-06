using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private Vector2 moveInput;

        // components
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GameManager.Instance.OnGameOver += LockMovement;
            GameManager.Instance.OnGameStarted += UnlockMovement;
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void SetMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        private void Move()
        {
            rb.linearVelocity = moveInput * speed;
        }

        private void LockMovement()
        {
            rb.linearVelocity = Vector2.zero;

            this.enabled = false;
        }

        private void UnlockMovement()
        {
            rb.linearVelocity = Vector2.zero;

            this.enabled = true;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameOver -= LockMovement;
            GameManager.Instance.OnGameStarted -= UnlockMovement;
        }
    }
}
