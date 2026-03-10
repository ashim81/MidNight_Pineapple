using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    public int damageFromPlayer = 5;

    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(damageFromPlayer, collision.transform);
        }
    }
}