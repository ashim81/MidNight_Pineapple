using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float knockbackForce = 5f;

    private Rigidbody2D rb;

    void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // ✅ ONLY DAMAGE FROM ENEMY
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10, collision.transform);
        }
    }

    public void TakeDamage(int damage, Transform attacker = null)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage.");

        // Apply knockback
        if (attacker != null && rb != null)
        {
            Vector2 direction = (transform.position - attacker.position).normalized;
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");

        GameManager gm = FindFirstObjectByType<GameManager>();

        if (gm != null)
        {
            gm.ShowWin();
        }

        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}