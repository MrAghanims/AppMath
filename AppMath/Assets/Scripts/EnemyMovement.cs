using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public float speed = 2f;
    public float chaseRange = 5f;
    public Transform player;
    private bool isChasing = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < chaseRange)
        {
            isChasing = true;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (isChasing && distance >= chaseRange + 3)
        {
            isChasing = false;
        }

        if (!isChasing)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}