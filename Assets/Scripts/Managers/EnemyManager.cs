using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] private int deadEnemiesCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
    }

    public void IncreaseDeadEnemiesCount()
    {
        deadEnemiesCount++;

        CheckIfAllEnemiesAreDead();
    }

    private void CheckIfAllEnemiesAreDead()
    {
        if (AreAllEnemiesDead())
        {
            GameManager.Instance.GameOver(GameManager.GameOverType.Win);
        }
    }

    public bool AreAllEnemiesDead()
    {
        return deadEnemiesCount >= enemyList.Count;
    }
}
