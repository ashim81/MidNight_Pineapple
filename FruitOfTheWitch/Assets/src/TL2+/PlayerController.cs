using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // state machine
    private InternalStateMachine stateMachine;
    public float moveSpeed;
    public bool sneaky;


    // Component
    private Rigidbody2D rb;

    // values
    private Vector2 inputVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(gameObject);
        stateMachine = new InternalStateMachine();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = stateMachine.getMoveSpeed();
        sneaky = stateMachine.isSneaky();
        rb.linearVelocity = moveSpeed * inputVector;
    }

    // Events
    public void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    public void Sneak(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            stateMachine.RunCommand(InternalStateMachine.Command.Sneak);
        }
        else if (context.canceled)
        {
            stateMachine.RunCommand(InternalStateMachine.Command.Unsneak);
        }
    }
}
