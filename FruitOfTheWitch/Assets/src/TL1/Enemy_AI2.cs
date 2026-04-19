using UnityEngine;

public class Enemy_AI2 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private int health = 100;

    private bool isKnockedBack = false;

    [SerializeField]
    private WitchHealth witchhealth;

    // ADD 1 — reference to FireSpell
    public FireSpell fireSpell;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        witchhealth.SetMaxHealth(health);
    }

    // ADD 2 — call TryCast every frame
    void Update()
    {
        fireSpell?.TryCast(transform, player);
    }

    void FixedUpdate()
    {
        if (player == null) return;
        if (isKnockedBack)
        {
            isKnockedBack = false;
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) health = 0;
        witchhealth.SetHealth(health);
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    // Check Fireball FIRST — before anything else
    if (collision.gameObject.CompareTag("Fireball")) return;

    if (collision.gameObject.CompareTag("Player"))
    {
        TakeDamage(7);
        collision.gameObject.GetComponent<PlayerController>().TakeDamage(3);

        Vector2 dir = (transform.position - collision.transform.position).normalized;
        rb.AddForce(dir * 90f, ForceMode2D.Impulse);
        isKnockedBack = true;
    }
}
}