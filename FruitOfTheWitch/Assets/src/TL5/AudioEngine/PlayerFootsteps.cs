using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private AudioEngine audioEngine;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float stepInterval = 0.35f;
    [SerializeField] private float minMoveSpeed = 0.1f;

    private float stepTimer;

    private void Start()
    {
        stepTimer = stepInterval;
    }

    private void Update()
    {
        if (audioEngine == null || rb == null)
        {
            return;
        }

        bool isMoving = rb.linearVelocity.magnitude > minMoveSpeed;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                audioEngine.PlaySFXGame("Footstep");
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}