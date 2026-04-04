using UnityEngine;
using System.Collections;

public class Enemy_AI_TL6 : MonoBehaviour
{
    public Transform player;

    public float speed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 2.5f;
    public float patrolDistance = 3f;

    public int damage = 1;
    public float attackCooldown = 2f;

    private float lastAttackTime;
    private Vector2 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance <= chaseRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float left = startPos.x - patrolDistance;
        float right = startPos.x + patrolDistance;

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= right) movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= left) movingRight = true;
        }
    }

    void Chase()
    {
        FacePlayer();

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    void Attack()
    {
        FacePlayer();

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(Hit());
            lastAttackTime = Time.time;
        }
    }

    IEnumerator Hit()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        transform.position += (Vector3)(dir * 0.4f);
        yield return new WaitForSeconds(0.1f);

        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null) pc.TakeDamage(damage);

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = dir * 5f;

        yield return new WaitForSeconds(0.1f);

        transform.position -= (Vector3)(dir * 0.4f);
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
}