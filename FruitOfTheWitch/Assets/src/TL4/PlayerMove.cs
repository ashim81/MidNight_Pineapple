using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveSpeed * moveInput;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}