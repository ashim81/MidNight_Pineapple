using UnityEngine;

public class EnemyLevel1 : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float detectionRange = 6f;
    public float stopDistance = 1f;

    private Vector2 startPosition;
    private SpriteRenderer sr;

    void Start()
    {
        startPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            Vector2 direction = player.position - transform.position;

            // Turn toward player
            if (direction.x > 0)
                sr.flipX = true;
            else
                sr.flipX = false;

            // Chase player
            if (distance > stopDistance)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    speed * Time.deltaTime
                );
            }
        }
        else
        {
            // Return to original position
            transform.position = Vector2.MoveTowards(
                transform.position,
                startPosition,
                speed * Time.deltaTime
            );
        }
    }
}