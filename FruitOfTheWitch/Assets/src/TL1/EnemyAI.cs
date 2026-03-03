using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;

    public float jumpForce = 7f;
    public float jumpCooldown = 2f;

    private Rigidbody2D rb;
    private EnemyAttack enemyAttack;

    private float lastJumpTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceX = player.position.x - transform.position.x;

        // Move horizontally toward player
        if (Mathf.Abs(distanceX) > attackRange)
        {
            float direction = Mathf.Sign(distanceX);
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            if (enemyAttack != null)
            {
                enemyAttack.TryAttack(player);
            }
        }

        // Random Jump Logic
        if (Time.time > lastJumpTime + jumpCooldown)
        {
            if (Random.value < 0.3f) // 30% chance to jump
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                lastJumpTime = Time.time;
            }
        }
    }
}