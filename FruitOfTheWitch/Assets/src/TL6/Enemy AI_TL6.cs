using UnityEngine;

public class EnemyAI_TL6 : MonoBehaviour
{
    public Transform player;

    public float speed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1.5f;

    public int health = 50;

    private float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    void Update()
    {
        if (player == null) return;
        if (health <= 0) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance <= chaseRange)
        {
            Chase();
        }
    }

    void Chase()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        if (player != null)
        {
            player.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
        }

        lastAttackTime = Time.time;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}