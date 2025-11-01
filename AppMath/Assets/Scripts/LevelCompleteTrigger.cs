using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public string nextLevelName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the panel
            Debug.Log("Game quit!");
            if (levelCompletePanel != null)
                levelCompletePanel.SetActive(true);

            // Pause game and unlock cursor
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Called by the Next Level button
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevelName);
    }
}