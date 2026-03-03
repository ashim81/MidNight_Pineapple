using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;

    private Rigidbody2D rb;
    private EnemyAttack enemyAttack;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceX = player.position.x - transform.position.x;

        // If outside attack range → move toward player
        if (Mathf.Abs(distanceX) > attackRange)
        {
            float direction = Mathf.Sign(distanceX);
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Stop horizontal movement but keep gravity
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            if (enemyAttack != null)
            {
                enemyAttack.TryAttack(player);
            }
        }
    }
}