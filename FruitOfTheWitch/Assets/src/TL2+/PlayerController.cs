using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;

    // Component
    private Rigidbody2D rb;

    // values
    private Vector2 inputVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveSpeed * inputVector;
    }

    // Events
    public void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }
}
