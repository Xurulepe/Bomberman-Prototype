using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
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
                    continue; // Skip spawning in safe zones
                }

                if (Random.Range(0, 100) >= spawnChancePercent)
                {
                    continue; // Skip spawning based on chance
                }

                bool isYEvenOrZero = y % 2 == 0 || y == 0;
                bool isXEvenOrZero = x % 2 == 0 || x == 0;

                if (isYEvenOrZero && isXEvenOrZero)
                {
                    continue; // Skip spawning on even coordinates
                }

                Instantiate(spawnPrefab, spawnPosition, Quaternion.identity, spawnParent);
            }
        }
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
}
