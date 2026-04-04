using UnityEngine;

public class Enemy_AI_TL6 : MonoBehaviour
{
    public Transform player;

    public float speed = 2f;
    public float chaseRange = 5f;

    // Increased so colliders don't block the attack state
    public float attackRange = 3f;

    public float patrolDistance = 3f;

    public int damage = 1;
    public float attackCooldown = 2f;

    private float lastAttackTime = 0f;

    private Vector2 startPos;
    private bool movingRight = true;

    private enum State { Patrol, Chase, Attack }
    private State currentState;

    private Animator animator;

    void Start()
    {
        startPos = transform.position;
        currentState = State.Patrol;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > chaseRange)
        {
            currentState = State.Patrol;
        }
        else if (distance > attackRange)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Attack;
        }

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        float leftLimit = startPos.x - patrolDistance;
        float rightLimit = startPos.x + patrolDistance;

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftLimit)
                movingRight = true;
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

        // Debug to confirm Attack state is reached
        Debug.Log(">>> ATTACK STATE ENTERED <<<");

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetTrigger("Attack");

            Debug.Log("Enemy Attacked!");

            PlayerController health = player.GetComponent<PlayerController>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
        }
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sound"))
        {
            currentState = State.Chase;
        }
    }
}
