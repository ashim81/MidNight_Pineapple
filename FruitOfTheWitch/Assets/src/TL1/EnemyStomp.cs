using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    public int stompDamage = 50;

    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Check if player is falling down
            if (playerRb != null && playerRb.linearVelocity.y < 0)
            {
                // Check if player is above enemy
                if (collision.transform.position.y > transform.position.y + 0.5f)
                {
                    health.TakeDamage(stompDamage, collision.transform);

                    // Bounce player upward
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 8f);
                }
            }
        }
    }
}