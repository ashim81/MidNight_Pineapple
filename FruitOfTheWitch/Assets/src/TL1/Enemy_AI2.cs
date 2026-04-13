using UnityEngine;

public class Enemy_AI2 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private int health = 100;

    [SerializeField]
    private WitchHealth witchhealth;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        witchhealth.SetMaxHealth(health);
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    // Make this public so other scripts can call it if needed
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
        }

        witchhealth.SetHealth(health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TakeDamage(10);
        }
    }
}