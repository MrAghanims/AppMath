using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        // Show the Game Over UI
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Optional: stop time or player movement
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // Restart without changing scenes
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // You can respawn the player here if needed
        Debug.Log("Restarting...");
    }

    public void MainMenu()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("MainMenu");
    }
}