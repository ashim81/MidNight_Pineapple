using UnityEngine;
using System.Collections.Generic;

public class TL3_EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> patrolWaypoints;

    [Header("Speeds")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolSpeed = 1.5f;

    [Header("Movement Smoothness")]
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Ranges")]
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float waypointBuffer = 0.2f;

    [Header("Patrol Settings")]
    private int currentWaypointIndex = 0;
    private bool forwardThroughWaypoints = true;

    [Header("Alerted Settings")]
    private Vector2 lastHeardPosition;
    private bool hasAlertPosition = false;

    private SpriteRenderer sr;
    private Animator anim;
    private Vector2 moveDirection;
    private VisualDetector detector;

    private enum State { Patrol, Alerted, Chase, Attack }
    private State currentState;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        detector = GetComponentInChildren<VisualDetector>();
        currentState = State.Patrol;
    }

    void Update()
    {
        if (player == null) return;
        HandleStateSwitching();
        ExecuteStateAction();
        RotateVisionCone();
    }

    private void HandleStateSwitching()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (detector != null && detector.CheckIfAlerted())
        {
            currentState = (distanceToPlayer <= attackRange) ? State.Attack : State.Chase;
            hasAlertPosition = false; 
        }

        else if (hasAlertPosition || (detector != null && detector.CanSeePlayer()))
        {
            currentState = State.Alerted;

            if (detector != null && detector.CanSeePlayer()) 
            {
                lastHeardPosition = player.position;
                hasAlertPosition = true;
            }
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

            case State.Alerted:
                MoveTowards(lastHeardPosition, speed);
                SetAnimation(true, false);

                if (Vector2.Distance(transform.position, lastHeardPosition) < waypointBuffer)
                    hasAlertPosition = false;
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

    private void MoveTowards(Vector2 targetPos, float currentSpeed)
    {
        moveDirection = (targetPos - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(moveDirection * currentSpeed * Time.deltaTime);

        if (sr != null && moveDirection.x != 0)
        {
            sr.flipX = moveDirection.x < 0;
        }
    }

    public void OnHearSound(Vector2 soundPosition)
    {
        lastHeardPosition = soundPosition;
        hasAlertPosition = true;
    }

    private void AttackPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        if (sr != null) sr.flipX = dir.x < 0;

        // Attack Logic
    }

    private void SetAnimation(bool isMoving, bool isAttacking)
    {
        if (anim != null)
        {
            anim.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                float x = (Mathf.Abs(moveDirection.x) > 0.1f) ? Mathf.Sign(moveDirection.x) : 0;
                float y = (Mathf.Abs(moveDirection.y) > 0.1f) ? Mathf.Sign(moveDirection.y) : 0;

                anim.SetFloat("MoveX", x);
                anim.SetFloat("MoveY", y);
            }
        }
    }

    private void RotateVisionCone()
    {
        if (detector != null && moveDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            detector.transform.rotation = Quaternion.Lerp(
                detector.transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }
}