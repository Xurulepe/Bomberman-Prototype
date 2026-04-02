using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private float explosionDelay = 3f;
    [SerializeField] private int explosionDistance = 3;
    [SerializeField] private LayerMask explosionLayerMask;

    private float explosionTimer = 0f;
    private Collider2D[] objectsHit;

    private void OnEnable()
    {
        explosionTimer = 0f;
    }

    private void Update()
    {
        explosionTimer += Time.deltaTime;

        if (explosionTimer >= explosionDelay)
        {
            Explode();
        }
    }

    private void Explode()
    {
        objectsHit = GetObjectsInExplosionRange();

        foreach (Collider2D hit in objectsHit)
        {
            Debug.Log("Hit object: " + hit.name);
        }

        gameObject.SetActive(false);
    }

    private Collider2D[] GetObjectsInExplosionRange()
    {
        Collider2D[] allObjectsInExplosionRange;
        Collider2D[] objectsInHorizontalRange = Physics2D.OverlapBoxAll(transform.position, new Vector2(explosionDistance * 2f, 1f), 0f, explosionLayerMask);
        Collider2D[] objectsInVerticalRange = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, explosionDistance * 2f), 0f, explosionLayerMask);

        int allObjectsCount = objectsInHorizontalRange.Length + objectsInVerticalRange.Length;
        allObjectsInExplosionRange = new Collider2D[allObjectsCount];

        for (int i = 0; i < objectsInHorizontalRange.Length; i++)
        {
            allObjectsInExplosionRange[i] = objectsInHorizontalRange[i];
        }

        for (int i = 0; i < objectsInVerticalRange.Length; i++)
        {
            allObjectsInExplosionRange[objectsInHorizontalRange.Length + i] = objectsInVerticalRange[i];
        }

        return allObjectsInExplosionRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(explosionDistance * 2f, 1f));
        Gizmos.DrawWireCube(transform.position, new Vector2(1f, explosionDistance * 2f));
    }
}
