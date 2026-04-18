using UnityEngine;
using System.Collections.Generic;

public class TL3_EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> patrolWaypoints;
    [SerializeField] private VisualDetector detector; // optional

    [Header("Speeds")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolSpeed = 1.5f;

    [Header("Ranges")]
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float chaseRange = 6f;
    [SerializeField] private float waypointBuffer = 0.2f;

    private int currentWaypointIndex = 0;
    private bool forwardThroughWaypoints = true;

    private SpriteRenderer sr;
    private Animator anim;
    private Vector2 moveDirection;

    private float lastAttackTime = 0f;
    public float attackCooldown = 2f;

    private enum State { Patrol, Chase, Attack }
    private State currentState;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentState = State.Patrol;
    }

    void Update()
    {
        if (player == null) return;

        HandleStateSwitching();
        ExecuteStateAction();
    }

    // ================= STATE LOGIC =================
    private void HandleStateSwitching()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 🔴 Always attack if close (NO FAILURE)
        if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attack;
        }
        // 🟡 Use detector OR distance for chase
        else if ((detector != null && detector.CheckIfAlerted()) || distanceToPlayer <= chaseRange)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Patrol;
        }
    }

    private void ExecuteStateAction()
    {
        switch (currentState)
        {
            case State.Patrol:
                HandlePatrol();
                SetAnimation(true, false);
                break;

            case State.Chase:
                MoveTowards(player.position, speed);
                SetAnimation(true, false);
                break;

            case State.Attack:
                AttackPlayer();
                SetAnimation(false, true);
                break;
        }
    }

    // ================= PATROL =================
    private void HandlePatrol()
    {
        if (patrolWaypoints == null || patrolWaypoints.Count == 0) return;

        Transform targetWaypoint = patrolWaypoints[currentWaypointIndex];
        MoveTowards(targetWaypoint.position, patrolSpeed);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < waypointBuffer)
        {
            UpdateWaypointIndex();
        }
    }

    private void UpdateWaypointIndex()
    {
        if (forwardThroughWaypoints)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= patrolWaypoints.Count)
            {
                currentWaypointIndex = patrolWaypoints.Count - 2;
                forwardThroughWaypoints = false;
            }
        }
        else
        {
            currentWaypointIndex--;
            if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1;
                forwardThroughWaypoints = true;
            }
        }

        currentWaypointIndex = Mathf.Clamp(currentWaypointIndex, 0, patrolWaypoints.Count - 1);
    }

    // ================= MOVEMENT =================
    private void MoveTowards(Vector2 targetPos, float currentSpeed)
    {
        moveDirection = (targetPos - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(moveDirection * currentSpeed * Time.deltaTime);

        if (sr != null && moveDirection.x != 0)
        {
            sr.flipX = moveDirection.x < 0;
        }
    }

    // ================= ATTACK =================
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
            Debug.Log("Enemy ATTACKING!");

            // 🔴 Visual feedback for demo
            sr.color = Color.red;
            Invoke(nameof(ResetColor), 0.2f);

            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TakeDamage(10);
            }

            lastAttackTime = Time.time;
        }
    }

    void ResetColor()
    {
        if (sr != null)
            sr.color = Color.white;
    }

    // ================= ANIMATION =================
    private void SetAnimation(bool isMoving, bool isAttacking)
    {
        if (anim != null)
        {
            anim.SetBool("isMoving", isMoving);
            anim.SetBool("isAttacking", isAttacking);

            if (isMoving)
            {
                float x = (Mathf.Abs(moveDirection.x) > 0.1f) ? Mathf.Sign(moveDirection.x) : 0;
                float y = (Mathf.Abs(moveDirection.y) > 0.1f) ? Mathf.Sign(moveDirection.y) : 0;

                anim.SetFloat("MoveX", x);
                anim.SetFloat("MoveY", y);
            }
        }
    }
}