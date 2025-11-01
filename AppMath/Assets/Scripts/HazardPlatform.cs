using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardPlatform : MonoBehaviour
{
    [Header("Assign your Game Over panel here")]
    public GameObject gameOverPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player stepped on a hazard!");

            // Show Game Over panel
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            // Stop time (optional)
            Time.timeScale = 0f;
        }
    }
}