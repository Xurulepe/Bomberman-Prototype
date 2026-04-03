using UnityEngine;

public class BombExplosionBuffer : MonoBehaviour
{
    [SerializeField] private int distanceIncreaseAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerAttributes playerAttributes))
        {
            playerAttributes.IncreaseBombExplosionDistance(distanceIncreaseAmount);
            Destroy(gameObject);
        }
    }
}
