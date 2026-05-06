using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int score;

    public enum GameOverType
    {
        Win,
        Lose,
        Draw
    }

    private GameOverType currentGameOverType;
    public GameOverType CurrentGameOverType => currentGameOverType;
    public int Score => score;

    public event Action OnGameStarted;
    public event Action OnGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        OnGameStarted?.Invoke();

        score = 0;
    }

    public void GameOver(GameOverType gameOverType)
    {
        Debug.Log("Game Over!");
        currentGameOverType = gameOverType;

        OnGameOver?.Invoke();
    }

    public void AddScore(int points)
    {
        score += points;
    }
}
