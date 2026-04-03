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

    public void IncreaseMaxBombs(int amount)
    {
        maxBombs += amount;
    }

    public void IncreaseBombExplosionDistance(int amount)
    {
        bombExplosionDistance += amount;
    }
}
