using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    
    [Header("Grid Settings")]
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;
    [SerializeField] private int startPositionX;
    [SerializeField] private int startPositionY;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private int spawnChancePercent;
    [SerializeField] private List<Vector2> safeZones = new List<Vector2>();
    [SerializeField] private List<Vector2> freeToSpawnEnemyPositions = new List<Vector2>();

    public List<Vector2> FreeToSpawnEnemyPositions => freeToSpawnEnemyPositions;

    public event Action OnGridGenerated;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddCornersToSafeZones();

        GenerateGrid();
    }

    private void GenerateGrid()
    {  
        for (int x = startPositionX; x < gridSizeX + startPositionX; x++)
        {
            for (int y = startPositionY; y < gridSizeY + startPositionY; y++)
            {
                Vector3 spawnPosition = new Vector3(x, y, 0);

                if (safeZones.Contains(new Vector2(x, y)))
                {
                    continue; 
                }

                bool isYEvenOrZero = y % 2 == 0 || y == 0;
                bool isXEvenOrZero = x % 2 == 0 || x == 0;

                if (isYEvenOrZero && isXEvenOrZero)
                {
                    continue; 
                }

                if (UnityEngine.Random.Range(0, 100) >= spawnChancePercent)
                {
                    AddFreeToSpawnEnemyPosition(new Vector2(x, y));

                    continue; 
                }

                Instantiate(spawnPrefab, spawnPosition, Quaternion.identity, spawnParent);
            }
        }

        OnGridGenerated?.Invoke();
    }

    private void AddCornersToSafeZones()
    {
        // Bottom-left corner
        safeZones.Add(new Vector2(startPositionX, startPositionY)); 
        safeZones.Add(new Vector2 (startPositionX, startPositionY + 1));
        safeZones.Add(new Vector2(startPositionX + 1, startPositionY));

        // Bottom-right corner
        safeZones.Add(new Vector2(startPositionX + gridSizeX - 1, startPositionY)); 
        safeZones.Add(new Vector2(startPositionX + gridSizeX - 1, startPositionY + 1));
        safeZones.Add(new Vector2(startPositionX + gridSizeX - 2, startPositionY));

        // Top-left corner
        safeZones.Add(new Vector2(startPositionX, startPositionY + gridSizeY - 1)); 
        safeZones.Add(new Vector2(startPositionX, startPositionY + gridSizeY - 2));
        safeZones.Add(new Vector2(startPositionX + 1, startPositionY + gridSizeY - 1));

        // Top-right corner
        safeZones.Add(new Vector2(startPositionX + gridSizeX - 1, startPositionY + gridSizeY - 1)); 
        safeZones.Add(new Vector2(startPositionX + gridSizeX - 1, startPositionY + gridSizeY - 2));
        safeZones.Add(new Vector2(startPositionX + gridSizeX - 2, startPositionY + gridSizeY - 1));
    }

    private void AddFreeToSpawnEnemyPosition(Vector2 position)
    {
        // não adiciona a posição se ela for a primeira ou ultima coluna ou linha
        // para evitar que os inimigos sejam gerados nas bordas do grid, onde o jogador pode ficar preso
        bool isXOnEdge = position.x == startPositionX || position.x == startPositionX + gridSizeX - 1;
        bool isYOnEdge = position.y == startPositionY || position.y == startPositionY + gridSizeY - 1;

        if (isXOnEdge || isYOnEdge)
        {
            return;
        }

        if (!freeToSpawnEnemyPositions.Contains(position))
        {
            freeToSpawnEnemyPositions.Add(position);
        }
    }
}
