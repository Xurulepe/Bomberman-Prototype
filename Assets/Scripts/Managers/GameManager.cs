using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameOverType
    {
        Win,
        Lose,
        Draw
    }

    private GameOverType currentGameOverType;
    public GameOverType CurrentGameOverType => currentGameOverType;

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
        DontDestroyOnLoad(gameObject);

        OnGameStarted?.Invoke();
    }

    public void GameOver(GameOverType gameOverType)
    {
        Debug.Log("Game Over!");
        currentGameOverType = gameOverType;

        OnGameOver?.Invoke();
    }
}
