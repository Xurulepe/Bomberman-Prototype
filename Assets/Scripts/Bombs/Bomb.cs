using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private float explosionDelay = 3f;
    [SerializeField] private float explosionTimer = 0f;

    private void Update()
    {
        explosionTimer += Time.deltaTime;

        if (explosionTimer >= explosionDelay)
        {
            Explode();

            explosionTimer = 0f;
        }
    }

    private void Explode()
    {
        print("Boom! The bomb has exploded.");
        
        gameObject.SetActive(false);
    }
}
