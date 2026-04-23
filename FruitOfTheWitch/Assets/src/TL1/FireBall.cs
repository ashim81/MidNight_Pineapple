using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    public void SetDirection(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
    }

    // Reset fireball state when pulled from pool
    public void ResetFireball()
    {
        direction = Vector2.zero;
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>()?.TakeDamage(10);
            // Return to pool instead of Destroy
            FireballPool.Instance.ReturnToPool(gameObject);
        }
    }

    // Auto return to pool if it flies too far
    void OnBecameInvisible()
    {
        FireballPool.Instance.ReturnToPool(gameObject);
    }
}