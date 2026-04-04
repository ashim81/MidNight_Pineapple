using UnityEngine;

public class EnemyAI_TL6 : MonoBehaviour
{
    public Transform player;

    public float speed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1.8f;
    public float soundRange = 6f;

    public float returnRange = 8f; // 🔥 NEW (IMPORTANT)

    public float patrolDistance = 3f;

    private Vector2 startPos;
    private bool movingRight = true;

    public float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    private SpriteRenderer sr;
    private Animator anim;

    private enum State { Patrol, Alerted, Chase, Attack, Return }
    private State currentState;

    void Start()
    {
        startPos = transform.position;
        currentState = State.Patrol;

        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceFromStart = Vector2.Distance(transform.position, startPos);

        // -------- STATE SWITCH --------
        if (distanceFromStart > returnRange)
        {
            currentState = State.Return;
        }
        else if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attack;
        }
        else if (distanceToPlayer <= chaseRange)
        {
            currentState = State.Chase;
        }
        else if (distanceToPlayer <= soundRange)
        {
            currentState = State.Alerted;
        }
        else
        {
            currentState = State.Patrol;
        }

        // -------- STATE ACTION --------
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                SetAnimation(false, false);
                break;

            case State.Alerted:
                MoveToPlayer(distanceToPlayer);
                SetAnimation(true, false);
                break;

            case State.Chase:
                MoveToPlayer(distanceToPlayer);
                SetAnimation(true, false);
                break;

            case State.Attack:
                AttackPlayer();
                SetAnimation(false, true);
                break;

            case State.Return:
                ReturnToStart();
                SetAnimation(true, false);
                break;
        }
    }

    // -------- PATROL --------
    void Patrol()
    {
        float move = movingRight ? 1 : -1;

        transform.position += new Vector3(move * speed * Time.deltaTime, 0, 0);

        if (Vector2.Distance(transform.position, startPos) > patrolDistance)
        {
            movingRight = !movingRight;
        }

        if (sr != null)
        {
            sr.flipX = move < 0;
        }
    }

    // -------- MOVE TO PLAYER --------
    void MoveToPlayer(float distance)
    {
        if (distance <= attackRange)
            return;

        Vector2 direction = (player.position - transform.position).normalized;

        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        if (sr != null)
        {
            if (direction.x > 0.1f)
                sr.flipX = false;
            else if (direction.x < -0.1f)
                sr.flipX = true;
        }
    }

    // -------- RETURN TO START --------
    void ReturnToStart()
    {
        Vector2 direction = (startPos - (Vector2)transform.position).normalized;

        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        if (sr != null)
        {
            if (direction.x > 0.1f)
                sr.flipX = false;
            else if (direction.x < -0.1f)
                sr.flipX = true;
        }

        // reached home
        if (Vector2.Distance(transform.position, startPos) < 0.1f)
        {
            currentState = State.Patrol;
        }
    }

    // -------- ATTACK --------
    void AttackPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        if (sr != null)
        {
            if (direction.x > 0.1f)
                sr.flipX = false;
            else if (direction.x < -0.1f)
                sr.flipX = true;
        }

        if (Time.time - lastAttackTime > attackCooldown)
        {
            PlayerController pc = player.GetComponent<PlayerController>();

            if (pc != null)
            {
                pc.TakeDamage(10);
                Debug.Log("Enemy attacked player!");
            }

            lastAttackTime = Time.time;
        }
    }

    // -------- ANIMATION --------
    void SetAnimation(bool isMoving, bool isAttacking)
    {
        if (anim == null) return;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttacking);
    }
}