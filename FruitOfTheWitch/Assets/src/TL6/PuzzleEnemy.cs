using UnityEngine;

public class PuzzleEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform player;

    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float waypointBuffer = 0.1f;

    private int currentIndex = 0;

    void Start()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("Patrol points not assigned!");
            return;
        }

        // Start exactly at first point
        transform.position = patrolPoints[0].position;

        // Move to next point
        currentIndex = 1;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // ATTACK (STOP MOVEMENT)
        if (distance <= attackRange)
        {
            Debug.Log("Enemy Attacking");
            return;
        }

        // PATROL
        Patrol();
    }

    void Patrol()
    {
        Transform target = patrolPoints[currentIndex];

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            patrolSpeed * Time.deltaTime
        );

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < waypointBuffer)
        {
            // Snap to exact point
            transform.position = target.position;

            currentIndex++;

            if (currentIndex >= patrolPoints.Length)
                currentIndex = 0;
        }
    }
}
