using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [Header("Game Over Menu")]
    [SerializeField] private Menu gameOverMenu;
    [SerializeField] private GameObject backgroundPanel;

    [Header("Game Over Info")]
    [SerializeField] private TextMeshProUGUI resultText;

    private void Start()
    {
        GameManager.Instance.OnGameOver += ShowGameOverMenu;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameOver -= ShowGameOverMenu;
        }
    }

    private void ShowGameOverMenu()
    {
        UpdateGameOverInfo();

        backgroundPanel.SetActive(true);
        gameOverMenu.Open();
    }

    private void UpdateGameOverInfo()
    {
        if (GameManager.Instance.CurrentGameOverType == GameManager.GameOverType.Win)
        {
            resultText.text = "Victory!";
        }
        else if (GameManager.Instance.CurrentGameOverType == GameManager.GameOverType.Lose)
        {
            resultText.text = "Defeat";
        }
        else
        {
            resultText.text = "Draw!";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0); 
    }
}
