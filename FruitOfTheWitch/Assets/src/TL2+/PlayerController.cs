using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // state machine
    private InternalStateMachine stateMachine;
    [SerializeField]
    private float moveSpeed;
    
    private bool sneaky;
    private int exhaustion = 0;

    

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
    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    public void OnCrouch(InputValue value)
    {
        stateMachine.RunCommand(InternalStateMachine.Command.ToggleSneak);
    }

    // Wrappers
    public bool isSneaky()
    {
        return sneaky;
    }
    
}
