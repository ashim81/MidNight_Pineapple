using UnityEngine;

public class Enemy_AI2 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private int health = 100;
    [SerializeField]
    private WitchHealth witchHealth;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
        private void TakeDamage(int damage)
    {
        health -= damage;
        if(health >= 0)
        {
            health = 0;
        }
        witchHealth.SetHealth(health); 
    }
}