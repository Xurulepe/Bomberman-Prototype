using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private LayerMask wallsLayerMask;
    [SerializeField] private float wallCheckDistance = 0.5f;

    private Vector2 moveDirection = Vector2.right;
    private List<Vector2> directions = new List<Vector2>();

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ChangeDirection();
    }

    private void Update()
    {
        CheckWalls();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void CheckWalls()
    {
        if (Physics2D.Raycast(transform.position, moveDirection, wallCheckDistance, wallsLayerMask))
        {
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
    }
}
