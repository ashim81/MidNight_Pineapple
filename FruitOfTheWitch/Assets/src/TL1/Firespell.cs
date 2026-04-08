using UnityEngine;

// Parent CLASS (handles movement)
public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    protected Vector2 direction;

    // called from EnemyAI
    public virtual void SetDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    // movement every frame
    protected virtual void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}

// FIREBALL CLASS
public class FireSpell : Projectile
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // DAMAGE PLAYER  
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(5, transform);
            }

            Destroy(gameObject);
        }
    }
}