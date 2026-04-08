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
    private int exhaustion = 300;
    private bool isExhausted = false;
    private int health = 100;
    public HealthBar healthBar; 
    public StaminaBar staminabar;  
    

    // Component
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator; //tl5: aded for animation

    // values
    private Vector2 inputVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new InternalStateMachine();
        animator = GetComponent<Animator>(); //tl5: added for animation
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleStealth();
        HandleSprinting();
        HandleHealth();
        HandleAnimation();
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

    public void OnCrouch(InputValue value)
    {
        stateMachine.RunCommand(InternalStateMachine.Command.ToggleSneak);
    }

    private void HandleSprinting()
    {
        staminabar.SetStamina(exhaustion);
        if (exhaustion <= 300 && isExhausted)
        {
            exhaustion += 1;
        } else if (isExhausted == false && exhaustion >= 0)
        {
            exhaustion -= 1;
        }
        if (exhaustion > 300) exhaustion = 300;
        if (exhaustion < 0) {
            exhaustion = 0;
            isExhausted = true;
        };
        if (isExhausted)
        {
            stateMachine.RunCommand(InternalStateMachine.Command.StopRunning);
        }
    }

    public void OnSprint(InputValue value)
    {
        if (exhaustion >= 300)
        {
            isExhausted = false;
            stateMachine.RunCommand(InternalStateMachine.Command.ToggleRunning);
        }
    }

    private void HandleHealth()
    {
        if (health <= 0)
        {
            Respawn();
        }
    }

    private void HandleAnimation()
    {
        //tl5: added for animation
        animator.SetBool("IsMoving", inputVector.magnitude > 0.1f);
        animator.SetFloat("MoveX", inputVector.x);
        animator.SetFloat("MoveY", inputVector.y);

        
        if (inputVector.x > 0.1f)
        {
            sr.flipX = false;
        }
        else if (inputVector.x < -0.1f)
        {
            sr.flipX = true;
        }
    }

    // Events
    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
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
        healthBar.SetHealth(health);
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        health = 100;
        exhaustion = 0;
        stateMachine.RunCommand(InternalStateMachine.Command.Reset);
    }
}
