using System;
using System.Collections;
using UnityEngine;
using Game.Player;

public class Bomb : MonoBehaviour, IDestroyable
{
    [Header("Bomb Settings")]
    [SerializeField] private float explosionDelay = 3f;
    [SerializeField] private int explosionDistance = 3;
    [SerializeField] private float explosionThickness = 0.7f;
    [SerializeField] private LayerMask explosionLayerMask;  // damageable & destructible objects + player layers
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask obstaclesLayerMask;  // destructible and indestructible objects

    private float explosionTimer = 0f;
    private bool hasExploded = false;
    private Collider2D[] objectsHit;

    public event Action OnBombExploded;

    private void OnEnable()
    {
        explosionTimer = 0f;
        hasExploded = false;

        StartCoroutine(IgnoreCollisionWithPlayersUntilExit());
    }

    private void Update()
    {
        explosionTimer += Time.deltaTime;

        if (explosionTimer >= explosionDelay)
        {
            Explode();
        }
    }

    public void SetupBomb(PlayerAttributes playerAttributes)
    {
        explosionDelay = playerAttributes.BombExplosionDelay;
        explosionDistance = playerAttributes.BombExplosionDistance;
    }

    public void Destroy()
    {
        if (!hasExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        hasExploded = true;

        OnBombExploded?.Invoke();

        objectsHit = GetObjectsInExplosionRange();

        foreach (Collider2D hit in objectsHit)
        {
            if (hit == null || hit.gameObject == gameObject)
            {
                continue;
            }

            if (hit.TryGetComponent(out IDestroyable destroyable))
            {
                destroyable.Destroy();
            }
        }

        OnBombExploded = null;

        gameObject.SetActive(false);
    }

    private Collider2D[] GetObjectsInExplosionRange()
    {
        Collider2D[] allObjectsInExplosionRange;

        int rightRangeExplosionDistance = CountCells(Vector2.right);
        int leftRangeExplosionDistance = CountCells(Vector2.left);
        int upRangeExplosionDistance = CountCells(Vector2.up);
        int downRangeExplosionDistance = CountCells(Vector2.down);

        Vector3 startPosition;

        startPosition = transform.position + Vector3.right * rightRangeExplosionDistance * 0.5f;
        Collider2D[] objectsInRightRange = Physics2D.OverlapBoxAll(startPosition, new Vector2(rightRangeExplosionDistance, explosionThickness), 0f, explosionLayerMask);

        startPosition = transform.position + Vector3.left * leftRangeExplosionDistance * 0.5f;
        Collider2D[] objectsInLeftRange = Physics2D.OverlapBoxAll(startPosition, new Vector2(leftRangeExplosionDistance, explosionThickness), 0f, explosionLayerMask);

        startPosition = transform.position + Vector3.up * upRangeExplosionDistance * 0.5f;
        Collider2D[] objectsInUpRange = Physics2D.OverlapBoxAll(startPosition, new Vector2(explosionThickness, upRangeExplosionDistance), 0f, explosionLayerMask);

        startPosition = transform.position + Vector3.down * downRangeExplosionDistance * 0.5f;
        Collider2D[] objectsInDownRange = Physics2D.OverlapBoxAll(startPosition, new Vector2(explosionThickness, downRangeExplosionDistance), 0f, explosionLayerMask);

        int allObjectsCount = objectsInRightRange.Length + objectsInLeftRange.Length + objectsInUpRange.Length + objectsInDownRange.Length;
        allObjectsInExplosionRange = new Collider2D[allObjectsCount];

        for (int i = 0; i < objectsInRightRange.Length; i++)
        {
            allObjectsInExplosionRange[i] = objectsInRightRange[i];
        }

        for (int i = 0; i < objectsInLeftRange.Length; i++)
        {
            int index = objectsInRightRange.Length + i;
            allObjectsInExplosionRange[index] = objectsInLeftRange[i];
        }

        for (int i = 0; i < objectsInUpRange.Length; i++)
        {
            int index = objectsInRightRange.Length + objectsInLeftRange.Length + i;
            allObjectsInExplosionRange[index] = objectsInUpRange[i];
        }

        for (int i = 0; i < objectsInDownRange.Length; i++)
        {
            int index = objectsInRightRange.Length + objectsInLeftRange.Length + objectsInUpRange.Length + i;
            allObjectsInExplosionRange[index] = objectsInDownRange[i];
        }

        return allObjectsInExplosionRange;
    }

    private int CountCells(Vector2 direction)
    {
        RaycastHit2D firstObstacle = Physics2D.Raycast(transform.position, direction, explosionDistance, obstaclesLayerMask);

        if (firstObstacle.collider != null)
        {
            return Mathf.RoundToInt(Vector2.Distance(transform.position, firstObstacle.collider.transform.position));
        }

        return explosionDistance;
    }

    private IEnumerator IgnoreCollisionWithPlayersUntilExit()
    {
        Collider2D bombCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, Vector2.one, 0f, playerLayerMask);

        if (playerCollider == null)
        {
            yield break; 
        }

        Physics2D.IgnoreCollision(bombCollider, playerCollider, true);

        // Espera até não estarem mais sobrepostos
        yield return new WaitUntil(() => !bombCollider.bounds.Intersects(playerCollider.bounds));

        Physics2D.IgnoreCollision(bombCollider, playerCollider, false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(explosionDistance * 2f, explosionThickness));
        Gizmos.DrawWireCube(transform.position, new Vector2(explosionThickness, explosionDistance * 2f));
    }
}
