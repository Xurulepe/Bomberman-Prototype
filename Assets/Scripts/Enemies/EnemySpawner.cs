using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemyCount;

    private void Start()
    {
        GridManager.Instance.OnGridGenerated += SpawnEnemies;
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (GridManager.Instance.FreeToSpawnEnemyPositions.Count == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, GridManager.Instance.FreeToSpawnEnemyPositions.Count);
            Vector2 spawnPosition = GridManager.Instance.FreeToSpawnEnemyPositions[randomIndex];

            GameObject enemy = GetRandomEnemy();
            
            if (enemy != null)
            {
                enemy.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
                enemy.SetActive(true);
            }
        }
    }

    private GameObject GetRandomEnemy()
    {
        int randomEnemyIndex = Random.Range(0, 2);
        
        if (randomEnemyIndex == 0)
        {
            return SlimeEnemyPool.Instance.GetPooledObject();
        }
        else
        {
            return BalloonEnemyPool.Instance.GetPooledObject();
        }
    }
}
