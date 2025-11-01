using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // for feedback UI

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public TextMeshProUGUI feedbackText;

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

        foreach (Collider enemy in hitEnemies)
        {
            Vector3 enemyForward = enemy.transform.forward;
            Vector3 directionToPlayer = (transform.position - enemy.transform.position).normalized;

            // Dot product: > 0 = in front, < 0 = behind
            float dot = Vector3.Dot(enemyForward, directionToPlayer);

            if (dot < 0) // player is behind enemy
            {
                feedbackText.text = "Backstab Successful!";
                Debug.Log("Backstab Successful");
                // You can destroy enemy or play animation
                Destroy(enemy.gameObject);

            }
            else
            {
                feedbackText.text = "Attack Failed!";
                Debug.Log("Attack Failed");
            }

            Invoke(nameof(ClearFeedback), 1f);
        }
    }

    void ClearFeedback()
    {
        StartCoroutine(ShowFeedback(""));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    IEnumerator ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.alpha = 1;

        yield return new WaitForSeconds(1f);

        // Fade out
        for (float t = 1f; t >= 0; t -= Time.deltaTime)
        {
            feedbackText.alpha = t;
            yield return null;
        }
        feedbackText.text = "";
    }
}