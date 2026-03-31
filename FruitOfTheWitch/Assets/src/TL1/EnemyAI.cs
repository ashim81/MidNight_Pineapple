using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject fireSpellPrefab;
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;

    public float jumpForce = 7f;
    public float jumpCooldown = 2f;

    private Rigidbody2D rb;
    private EnemyAttack enemyAttack;
    private Animator animator;

    private float lastJumpTime;
    private bool isGrounded;

    void ShootFireSpell()
{
    if (fireSpellPrefab == null || player == null) return;

    GameObject spell = Instantiate(fireSpellPrefab, transform.position, Quaternion.identity);

    FireSpell fs = spell.GetComponent<FireSpell>();

    if (fs != null)
    {
        fs.SetDirection(player.position);
    }
}
if (Time.time > lastShootTime + shootCooldown)
{
    ShootFireSpell();
    lastShootTime = Time.time;
}

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<EnemyAttack>();
        animator = GetComponent<Animator>(); // ✅ NEW
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
            if (Random.value < 0.3f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                lastJumpTime = Time.time;
            }
        }

        // 🔥 ANIMATION CONTROL
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        float velocityX = rb.linearVelocity.x;

        // Speed parameter
        animator.SetFloat("Speed", velocityX);

        // Jump
        animator.SetBool("isJumping", !isGrounded);

        // Direction-based animation
        if (velocityX > 0.1f)
        {
            animator.Play("Walk_right");
        }
        else if (velocityX < -0.1f)
        {
            animator.Play("Walk_left");
        }
    }

    // Ground detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}