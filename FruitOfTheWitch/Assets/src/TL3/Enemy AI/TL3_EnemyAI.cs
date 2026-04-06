using UnityEngine;
using System.Collections.Generic;

public class TL3_EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> patrolWaypoints;
    // currentTarget removed as we are using Vector2 targets for flexibility

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
    private VisualDetector detector; // Cached for performance

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

        // 1. CHASE/ATTACK (Visual bar is 100% full)
        if (detector != null && detector.CheckIfAlerted())
        {
            currentState = (distanceToPlayer <= attackRange) ? State.Attack : State.Chase;
            hasAlertPosition = false; 
        }
        // 2. ALERTED (Either currently seeing them OR moving to where they were)
        else if (hasAlertPosition || (detector != null && detector.CanSeePlayer()))
        {
            currentState = State.Alerted;

            // If we actively see them, keep updating the "Last Known" to their current spot
            if (detector != null && detector.CanSeePlayer()) 
            {
                lastHeardPosition = player.position;
                hasAlertPosition = true; // Keep this true while seeing them!
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
                // If reached last known pos, go back to patrol
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
        // Ping-Pong Logic (A -> B -> C -> B -> A)
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
        
        // Safety for single waypoint or empty lists
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

    // Called by the NoiseMaker script via SendMessage
    public void OnHearSound(Vector3 soundPosition)
    {
        // Only react to sound if we aren't already chasing the player
        lastHeardPosition = (Vector2)soundPosition;
        hasAlertPosition = true;
    }

    private void AttackPlayer()
    {
        // Face the player during attack
        Vector2 dir = (player.position - transform.position).normalized;
        if (sr != null) sr.flipX = dir.x < 0;

        // Attack Logic... (keep your existing TakeDamage code here)
    }

    private void SetAnimation(bool isMoving, bool isAttacking)
    {
        if (anim != null)
        {
            anim.SetBool("isMoving", isMoving);
            anim.SetBool("isAttacking", isAttacking);
        }
    }

    private void RotateVisionCone()
    {
        if (detector != null && moveDirection != Vector2.zero)
        {
            // 1. Calculate the target angle based on movement
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // 2. Smoothly rotate from the current rotation to the target rotation
            // Time.deltaTime ensures the speed is consistent regardless of frame rate
            detector.transform.rotation = Quaternion.Lerp(
                detector.transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }
}