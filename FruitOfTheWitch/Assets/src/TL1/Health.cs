using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float knockbackForce = 5f;

    private Rigidbody2D rb;

    // damage cooldown
    private float damageCooldown = 0.5f;
    private float lastDamageTime = -1f;

    void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Transform attacker = null)
    {
        // prevent rapid damage
        if (Time.time < lastDamageTime + damageCooldown)
            return;

        lastDamageTime = Time.time;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage.");

        // Knockback
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