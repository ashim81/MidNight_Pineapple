using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // state machine
    private InternalStateMachine stateMachine;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector2 respawnPoint;
    [SerializeField]
    private bool BCmode = false;
    
    private bool sneaky;
    private int exhaustion = 0;
    private int health = 100;

    

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
        HandleMovement();
        HandleStealth();
        HandleSprinting();
        HandleHealth();
    }

    private void HandleMovement()
    {
        moveSpeed = stateMachine.getMoveSpeed();
        rb.linearVelocity = moveSpeed * inputVector;
    }

    private void HandleStealth()
    {
        sneaky = stateMachine.isSneaky();
    }

    private void HandleSprinting()
    {   
        if (exhaustion > 0) exhaustion--;
        if (exhaustion <= 0){
            stateMachine.RunCommand(InternalStateMachine.Command.StopRunning);
        }
    }
    private void HandleHealth()
    {
        if (health <= 0)
        {
            Respawn();
        }
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

    public void OnSprint(InputValue value)
    {
        stateMachine.RunCommand(InternalStateMachine.Command.ToggleRunning);
        exhaustion = 300;
    }

    // Wrappers
    public bool isSneaky()
    {
        return sneaky;
    }

    public int getExhaustion()
    {
        return exhaustion;
    }

    public int getHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        if (BCmode == false) health -= damage;
        if (health <= 0) health = 0;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        health = 100;
        exhaustion = 0;
        stateMachine.RunCommand(InternalStateMachine.Command.Reset);
    }
}
