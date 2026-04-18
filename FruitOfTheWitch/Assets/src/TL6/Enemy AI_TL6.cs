using UnityEngine;

public class EnemyAI_TL6 : MonoBehaviour
{
    public Transform player;

    public float speed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1.8f;
    public float soundRange = 6f;

    public float returnRange = 8f; 

    public float patrolDistance = 3f;

    private Vector2 startPos;
    private bool movingRight = true;

    public float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    private SpriteRenderer sr;
    private Animator anim;

    private enum State { Patrol, Alerted, Chase, Attack, Return }
    private State currentState;

    // TL3: This is For the Vision Detector Rotation
    private Vector2 moveDirection;

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
        
        // TL3: Call My Function
        RotateVisionCone();
    }

    // -------- PATROL --------
    void Patrol()
    {
        float move = movingRight ? 1 : -1;

        // TL3: Update My Variable
        moveDirection = new Vector2(move, 0);

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
        
        // TL3: Same as above but just being safe
        moveDirection = (player.position - transform.position).normalized;
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
        
        // TL3: Same as above but just being safe
        moveDirection = ((Vector2)startPos - (Vector2)transform.position).normalized;
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
        { if (direction.x > 0.1f)
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

        // PiggyBoi?!
        if (anim != null)
        {
            // Tell the animator if we are moving at all
            anim.SetBool("isMoving", isMoving);
            
            // If we are moving, send the direction vector to the Blend Tree
            if (isMoving)
            {
                anim.SetFloat("MoveX", moveDirection.x);
                anim.SetFloat("MoveY", moveDirection.y);
            }
        }
    }

    // TL3: Rotates the child vision cone
    void RotateVisionCone()
    {
        VisualDetector detector = GetComponentInChildren<VisualDetector>();
        if (detector != null && moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            detector.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}