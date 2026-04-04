using UnityEngine;

// 🔷 BASE CLASS
public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    protected Vector2 direction;

    public virtual void SetDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    protected virtual void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}

// 🔥 CHILD CLASS
public class FireSpell : Projectile
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10, transform);
            }

            Destroy(gameObject);
        }
    }
}