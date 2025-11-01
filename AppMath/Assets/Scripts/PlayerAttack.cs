using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public TextMeshProUGUI feedbackText;

    private Coroutine feedbackCoroutine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click to attack
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        bool hitSomething = false;

        foreach (Collider enemy in hitEnemies)
        {
            hitSomething = true;

            Vector3 enemyForward = enemy.transform.forward;
            Vector3 directionToPlayer = (transform.position - enemy.transform.position).normalized;

            float dot = Vector3.Dot(enemyForward, directionToPlayer);

            if (dot < 0) // Player is behind enemy
            {
                ShowFeedback("Backstab Successful!", Color.green);
                Debug.Log("Backstab Successful");
                Destroy(enemy.gameObject);
            }
            else
            {
                ShowFeedback("Attack Failed!", Color.red);
                Debug.Log("Attack Failed");
            }
        }

        if (!hitSomething)
        {
            ShowFeedback("No Target", Color.yellow);
        }
    }

    void ShowFeedback(string message, Color color)
    {
        // If a feedback coroutine is running, stop it first
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        feedbackCoroutine = StartCoroutine(FeedbackRoutine(message, color));
    }

    IEnumerator FeedbackRoutine(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        feedbackText.alpha = 1f;

        yield return new WaitForSeconds(1.5f); // show message for 1.5 seconds

        // Fade out smoothly
        for (float t = 1f; t >= 0; t -= Time.deltaTime)
        {
            feedbackText.alpha = t;
            yield return null;
        }

        feedbackText.text = "";
        feedbackCoroutine = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}