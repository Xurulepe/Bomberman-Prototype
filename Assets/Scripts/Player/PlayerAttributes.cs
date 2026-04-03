using System;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private int maxBombs = 1;
    [SerializeField] private float bombExplosionDelay = 4f;
    [SerializeField] private int bombExplosionDistance = 3;

    public int MaxBombs => maxBombs;
    public float BombExplosionDelay => bombExplosionDelay;
    public int BombExplosionDistance => bombExplosionDistance;

    public event Action OnPowerUpCollected;

    public void IncreaseMaxBombs(int amount)
    {
        maxBombs += amount;
        OnPowerUpCollected?.Invoke();
    }

    public void IncreaseBombExplosionDistance(int amount)
    {
        bombExplosionDistance += amount;
        OnPowerUpCollected?.Invoke();
    }
}
